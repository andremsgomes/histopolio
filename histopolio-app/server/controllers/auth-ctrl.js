const { readJSONFile, writeJSONFile } = require("./../utils/json-utils");

function getUser(email) {
  const users = readJSONFile("./data/Users.json");

  return users.find((user) => user.credentials.email === email);
}

function signup(req, res) {
  const { name, email, password } = req.body;

  if (getUser(email)) {
    return res
      .status(409)
      .json({ error: true, message: "Utilizador já existente" });
  }

  const credentials = {
    email: email,
    password: password,
  };

  const users = readJSONFile("./data/Users.json");
  const id = users.length + 1;

  const user = {
    id: id,
    credentials: credentials,
    name: name,
    points: 20,
  };

  users.push(user);
  writeJSONFile("./data/Users.json", users);

  return res.status(201).json(user);
}

function login(req, res) {
  const { email, password } = req.body;
  const user = getUser(email);

  if (!user) {
    return res
      .status(404)
      .json({ error: true, message: "Utilizador não encontrado" });
  }

  if (user.credentials.password !== password) {
    return res.status(403).json({ error: true, message: "Password errada" });
  }

  return res.status(200).json(user);
}

module.exports = {
  signup,
  login,
};
