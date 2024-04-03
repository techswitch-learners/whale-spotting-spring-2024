import Species from "./Species"

interface Sighting {
  id: number
  latitude: number
  longitude: number
  userName: string
  species: Species
  description: string
  imageUrl: string
  bodyOfWater: string
  sightingTimestamp: string
  verificationEvent: {
    admin: {
      userName: string
    }
    comment: string | null
    timestamp: string
  }
}

export default Sighting
