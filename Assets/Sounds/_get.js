import fs from "fs";

let i = 0;

fs.readdirSync("./assets/sounds").forEach((file) => {
  if (/\.(mp3|wav)$/i.test(file)) {
    console.log(file.replace(/\.(mp3|wav)$/gi, "") + ` = ${i},`);
    i++;
  }
});
