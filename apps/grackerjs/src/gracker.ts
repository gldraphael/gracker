import FingerprintJS from '@fingerprintjs/fingerprintjs'

export async function setupGracker()
{
  const data = {
    fingerprint: await getFingerprint(),
    timezone: Intl.DateTimeFormat().resolvedOptions().timeZone
  }
  const bodyJson = JSON.stringify(data)

  // send an event immediately, and after every 10 seconds
  await sendEvent(bodyJson)
  setInterval(async () => { // TODO: explore ways to do this in a web-worker
    await sendEvent(bodyJson)
  }, 10_000)
}


async function getFingerprint() : Promise<string> {
  const fingerprintJs = await FingerprintJS.load({ monitoring: false });
  const result = await fingerprintJs.get()

  return result.visitorId
}

async function sendEvent(bodyJson: string) {
  const URL = 'http://localhost:17702/v1/event'

  const response = await fetch(URL, {
    method: 'POST',
    cache: 'no-cache',
    credentials: 'omit',
    headers: {
      'Content-Type': 'application/json'
    },
    redirect: 'follow',
    referrerPolicy: 'unsafe-url',
    body: bodyJson
  });
  if(response.ok != true) {
    console.warn(response.statusText)
  }
}
