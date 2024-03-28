import BodyOfWater from "./BodyOfWater"
import Species from "./Species"
import Reaction from "./Reaction"
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
  reactions: Reaction
  currentUserReaction: string | null
}

export default Sighting
