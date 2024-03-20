interface Sighting {
  latitude: number | undefined
  longitude: number | undefined
  token: string | undefined
  speciesId: number | undefined
  description: string | undefined
  imageUrl: string | undefined
  bodyOfWaterId: number | undefined
  sightingTimeStamp: Date | undefined
}

export default Sighting
