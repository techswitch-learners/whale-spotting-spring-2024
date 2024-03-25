import BodyOfWater from "./BodyOfWater"
import Species from "./Species"

interface Sighting {
  id: number
  latitude: number
  longitude: number
  userName: string
  species: Species
  description: string
  imageUrl: string
  bodyOfWater: BodyOfWater
  sightingTimestamp: string
}

export default Sighting
