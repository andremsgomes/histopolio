const bcrypt = require("bcrypt");

const { readJSONFile, writeJSONFile } = require("./../utils/json-utils");

function getUser(email) {
  const users = readJSONFile("./data/Users.json");

  return users.find((user) => user.credentials.email === email);
}

function signup(req, res) {
  const { name, email, password } = req.body;

  if (!(name && email && password)) {
    return res
      .status(400)
      .send({ error: true, message: "Dados mal formatados" });
  }

  if (getUser(email)) {
    return res
      .status(409)
      .json({ error: true, message: "Utilizador já existente" });
  }

  // generate salt to hash password
  const salt = bcrypt.genSaltSync(10);

  // hash password
  const hashedPassword = bcrypt.hashSync(password, salt);

  const credentials = {
    email: email,
    password: hashedPassword,
  };

  const users = readJSONFile("./data/Users.json");
  const id = users.length + 1;

  const user = {
    id: id,
    credentials: credentials,
    name: name,
  };

  users.push(user);
  writeJSONFile("./data/Users.json", users);

  const returnUser = {
    id: user.id,
    name: user.name,
  };

  return res.status(201).json(returnUser);
}

function login(req, res) {
  const { email, password } = req.body;

  if (!(email && password)) {
    return res
      .status(400)
      .send({ error: true, message: "Dados mal formatados" });
  }

  const user = getUser(email);

  if (!user) {
    return res
      .status(404)
      .json({ error: true, message: "Utilizador não encontrado" });
  }

  if (!bcrypt.compareSync(password, user.credentials.password)) {
    return res.status(403).json({ error: true, message: "Password errada" });
  }

  const returnUser = {
    id: user.id,
    name: user.name,
  };

  return res.status(200).json(returnUser);
}

module.exports = {
  signup,
  login,
};
