import { useEffect, useState } from "react"
import { Col, Container, Row } from "react-bootstrap"
import { Link, useParams } from "react-router-dom"
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet"
import { Icon } from "leaflet"
import icon from "/favicon.ico"
import Weather from "../models/view/Weather"
import HotSpot from "../models/view/Hotspot"

function HotSpotView() {
  const { id } = useParams()
  const [hotSpot, setHotSpot] = useState<HotSpot>()
  const [weather, setWeather] = useState<Weather>()
  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<boolean>(false)

  useEffect(() => {
    fetch(`http://localhost:5280/hotspots/${id}`)
      .then((response) => response.json())
      .then((data) => setHotSpot(data[0]))
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }, [id])

  const handleSubmit = () => {
    setWeather(undefined)
    setError(false)

    const lat = hotSpot?.latitude
    const lon = hotSpot?.longitude

    if (lat && lon) {
      fetch(
        `https://api.open-meteo.com/v1/forecast?latitude=${lat}&longitude=${lon}&current=temperature_2m,relative_humidity_2m,apparent_temperature,weather_code,precipitation&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_sum,precipitation_probability_max`,
      )
        .then((response) => response.json())
        .then((data) => setWeather(data))
        .catch(() => setError(true))
        .finally(() => setLoading(false))
    }
  }
  useEffect(handleSubmit, [hotSpot])

  const arr = []
  if (weather) {
    for (let i = 0; i < 7; i++) {
      arr.push({
        date: weather.daily.time[i],
        minTemp: weather.daily.temperature_2m_min[i],
        maxTemp: weather.daily.temperature_2m_max[i],
        rain: weather.daily.precipitation_probability_max[i],
        icon: weather.daily.weather_code[i],
      })
    }
  }
  arr.map((x) => {
    return x.date, x.minTemp, x.maxTemp, x.rain, x.icon
  })

  const customIcon = new Icon({
    iconUrl: icon,
    iconSize: [24, 24],
  })

  return (
    <>
      {hotSpot && (
        <>
          <h1>{hotSpot.name}</h1>
          <h2>Country: {hotSpot.country}</h2>

          <MapContainer style={{ height: "20rem" }} center={[26.115, -16.523]} zoom={1} scrollWheelZoom={true}>
            <TileLayer
              attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
              url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <Marker key={hotSpot.id} position={[Number(hotSpot.latitude), Number(hotSpot.longitude)]} icon={customIcon}>
              <Popup>{hotSpot.name}</Popup>
            </Marker>
          </MapContainer>

          <main className="d-flex align-items-center justify-content-center text-center">
            {weather && (
              <div>
                <br />
                <h5>Current weather:</h5>
                <img
                  src={`${import.meta.env.BASE_URL}weather-icons/icon${weather.current.weather_code}.png`}
                  width="35"
                />
                <p>Temp: {weather.current.temperature_2m} °C</p>
                <br></br>
                <h5>Weather forecast for the next 7 days:</h5>
                <Container className="d-flex flex-row justify-content-between">
                  <Row md={7}>
                    {arr.map((x, i) => (
                      <Col key={i}>
                        <p>{new Date(x.date).toDateString()}</p>
                        <img src={`${import.meta.env.BASE_URL}weather-icons/icon${x.icon}.png`} width="35" />
                        <p>MinTemp: {Math.round(x.minTemp)}°C</p>
                        <p>MaxTemp: {Math.round(x.maxTemp)}°C</p>
                        <p>Rain probability: {x.rain}%</p>
                      </Col>
                    ))}
                  </Row>
                </Container>
              </div>
            )}
            {loading && <p>Loading...</p>}
            {error && <p>Couldn't load weather data</p>}
          </main>

          <ul>
            {hotSpot.viewingSuggestions.map((suggestion) => (
              <li>
                {" "}
                <Link to={suggestion.species.wikiLink}>Species: {suggestion.species.name}</Link>, Time of year:{" "}
                {suggestion.timeOfYear}, Platforms: {suggestion.platforms},
                <img src={suggestion.species.exampleImageUrl} width="300" />
              </li>
            ))}
          </ul>
        </>
      )}
    </>
  )
}

export default HotSpotView
