import FingerprintJS from '@fingerprintjs/fingerprintjs'

export async function setupGracker()
{
  const fingerprint = await getFingerprint()

  // send this to an API server
}


async function getFingerprint() : Promise<string> {
  const fingerprintJs = await FingerprintJS.load({ monitoring: false });
  const result = await fingerprintJs.get()

  return result.visitorId
}
