export const fetchBodyOfWater = async (latitude: string, longitude: string) => {
  const url = `https://api.bigdatacloud.net/data/reverse-geocode-client?latitude=${latitude}&longitude=${longitude}`
  return await fetch(url)
    .then((response) => response.json())
    .then(
      (data) =>
        data?.localityInfo?.informative?.find(
          (info: { name: string; description: string }) =>
            ["passage", "basin", "strait", "bight", "sound", "channel", "gulf", "bay", "sea", "ocean"].some(
              (bodyOfWaterType) => info.name.toLowerCase().includes(bodyOfWaterType),
            ) && !["time zone", "continent"].some((otherType) => info.description.toLowerCase().includes(otherType)),
        )?.name ?? "",
    )
}
