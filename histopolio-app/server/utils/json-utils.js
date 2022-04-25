const fs = require("fs");

function readJSONFile(filePath) {
  const data = JSON.parse(fs.readFileSync(filePath));
  return data;
};

function writeJSONFile(filePath, content) {
  fs.writeFileSync(filePath, JSON.stringify(content, null, 2));
};

module.exports = {
    readJSONFile,
    writeJSONFile,
};