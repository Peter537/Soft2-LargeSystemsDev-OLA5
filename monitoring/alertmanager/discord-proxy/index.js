const express = require("express");
const fetch = require("node-fetch");
const app = express();
app.use(express.json());

const DISCORD = process.env.DISCORD_WEBHOOK_URL;
if (!DISCORD) {
  console.error("DISCORD_WEBHOOK_URL not set");
  process.exit(1);
}

function buildMessage(payload) {
  const status = payload.status || "firing";
  const alerts = payload.alerts || [];
  let text = `Alertmanager: **${status}** — ${alerts.length} alert(s)\n`;
  for (const a of alerts.slice(0, 5)) {
    const name = a.labels?.alertname ?? "alert";
    const summary = a.annotations?.summary ?? a.annotations?.description ?? "";
    text += `**${name}** — ${summary}\n`;
  }
  return { content: text };
}

app.post("/alert", async (req, res) => {
  try {
    const body = buildMessage(req.body);
    await fetch(DISCORD, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(body),
    });
    res.status(200).send("ok");
  } catch (e) {
    console.error(e);
    res.status(500).send("error");
  }
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => console.log(`discord-proxy listening ${PORT}`));
