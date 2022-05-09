const express = require("express");
const bodyParser = require("body-parser");
const http = require("http");
const cors = require("cors");

const WebSocket = require("ws");

const { processMessage } = require("./controllers/websocket-ctrl");
const authRouter = require("./routes/auth");
const gameRouter = require("./routes/game");

const app = express();
const apiPort = 8080;

const server = http.createServer(app);
const wss = new WebSocket.Server({ server: server });

app.use(cors());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

wss.on("connection", function connection(ws) {
  console.log("A new client connected!");

  ws.on("message", function message(data) {
    processMessage(ws, data);
  });
});

app.get("/", (req, res) => {
  res.send("Hello World!");
});

app.use("/api/auth", authRouter);
app.use("/api/game", gameRouter);

server.listen(apiPort, () => console.log(`Server running on port ${apiPort}`));
