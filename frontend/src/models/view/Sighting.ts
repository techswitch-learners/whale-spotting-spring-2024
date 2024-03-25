interface Sighting {
  id: number
  latitude: number
  longitude: number
  species: {
    name: string
  }
  description: string
  imageUrl: string
  bodyOfWater: {
    name: string
  }
  sightingTimestamp: string
}

export default Sighting
