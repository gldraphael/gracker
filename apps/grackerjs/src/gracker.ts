import FingerprintJS from '@fingerprintjs/fingerprintjs'

export async function setupGracker()
{
  const URL = 'http://localhost:17702/v1/events'
  const data = {
    fingerprint: await getFingerprint(),
    timezone: Intl.DateTimeFormat().resolvedOptions().timeZone
  }
  const bodyJson = JSON.stringify(data)

  // TODO: explore performing this in a web-worker
  setInterval(async () => {
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
  }, 10_000)
}


async function getFingerprint() : Promise<string> {
  const fingerprintJs = await FingerprintJS.load({ monitoring: false });
  const result = await fingerprintJs.get()

  return result.visitorId
}
