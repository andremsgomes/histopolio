const { readJSONFile } = require('./../utils/json-utils');

async function getUser(email) {
    const users = readJSONFile("./data/Users.json");

    return users.find((user) => user.credentials.email === email);
}

async function login(req, res) {
    const { email, password } = req.body;
    const user = await getUser(email);

    if (!user) {
        return res.status(404).json({ error: true, message: "Utilizador não encontrado" });
    }

    if (user.credentials.password !== password) {
        return res.status(403).json({ error: true, message: "Password errada" });
    }

    return res.status(200).json({ error: false, data: user });
}

module.exports = {
    login,
};