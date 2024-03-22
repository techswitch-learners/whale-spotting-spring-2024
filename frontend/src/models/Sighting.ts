interface Sighting {
  latitude: number | null
  longitude: number | null
  speciesId: number | null
  description: string | null
  imageUrl: string | null
  bodyOfWaterId: number | null
  sightingTimeStamp: Date | null
}

export default Sighting
