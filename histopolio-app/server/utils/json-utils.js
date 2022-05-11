const fs = require("fs");

function readJSONFile(filePath) {
  try {
    const data = JSON.parse(fs.readFileSync(filePath));
    return data;
  } catch (error) {
    console.log(error);
    return null;
  }
}

function writeJSONFile(filePath, content) {
  fs.writeFileSync(filePath, JSON.stringify(content, null, 2));
}

module.exports = {
  readJSONFile,
  writeJSONFile,
};
