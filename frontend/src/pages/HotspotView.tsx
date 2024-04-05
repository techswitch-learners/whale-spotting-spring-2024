import { useEffect, useState } from "react"
import { Card, Col, Container, Row } from "react-bootstrap"
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet"
import { useParams } from "react-router-dom"
import { faDroplet, faTemperatureArrowDown, faTemperatureArrowUp } from "@fortawesome/free-solid-svg-icons"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { Icon } from "leaflet"
import { getHotspotById } from "../api/backendClient"
import { getWeather } from "../api/weatherClient"
import Hotspot from "../models/view/Hotspot"
import Weather from "../models/view/Weather"
import icon from "/favicon.ico"

const customIcon = new Icon({
  iconUrl: icon,
  iconSize: [24, 24],
})

function HotspotView() {
  const { id } = useParams()
  const [hotspot, setHotspot] = useState<Hotspot>()
  const [weather, setWeather] = useState<Weather>()
  const [loading, setLoading] = useState<boolean>(true)
  const [error, setError] = useState<boolean>(false)

  const fetchHotspotData = () => {
    getHotspotById(id)
      .then((response) => response.json())
      .then((data) => setHotspot(data))
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }
  useEffect(fetchHotspotData, [id])

  const loadWeather = () => {
    setWeather(undefined)

    const lat = hotspot?.latitude
    const lon = hotspot?.longitude

    if (lat !== undefined && lon !== undefined) {
      getWeather(lat, lon)
        .then((response) => response.json())
        .then((data) => setWeather(data))
        .catch(() => setError(true))
        .finally(() => setLoading(false))
    }
  }
  useEffect(loadWeather, [hotspot])

  const weatherData = []
  if (weather) {
    for (let i = 0; i < 7; i++) {
      weatherData.push({
        date: weather.daily.time[i],
        minTemp: weather.daily.temperature_2m_min[i],
        maxTemp: weather.daily.temperature_2m_max[i],
        rain: weather.daily.precipitation_probability_max[i],
        icon: weather.daily.weather_code[i],
      })
    }
  }

  return (
    <>
      {hotspot && weather && (
        <Container>
          <h1 className="text-center">{hotspot.name}</h1>
          <h2 className="text-center mb-4">{hotspot.country}</h2>
          <Row>
            <Col xs={12} md={4} className="d-flex flex-column align-items-center">
              <h5 className="text-center">Hotspot location</h5>
              <MapContainer
                className="mw-100"
                style={{ height: "20rem", width: "32rem" }}
                center={[30, hotspot.longitude]}
                zoom={1}
                scrollWheelZoom={true}
              >
                <TileLayer
                  attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                  url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                <Marker
                  key={hotspot.id}
                  position={[Number(hotspot.latitude), Number(hotspot.longitude)]}
                  icon={customIcon}
                >
                  <Popup>{hotspot.name}</Popup>
                </Marker>
              </MapContainer>
            </Col>
            <Col xs={12} md={8} className="d-flex flex-column align-items-center">
              <h5 className="text-center mt-3 mt-md-0">Weather forecast</h5>
              <div className="d-flex flex-wrap justify-content-center gap-3">
                {weatherData.map((dailyWeatherData) => (
                  <div key={dailyWeatherData.date} className="shadow p-3">
                    <Row style={{ width: "10rem" }}>
                      <Col className="text-start">
                        <h5 className="mb-0">{new Date(dailyWeatherData.date).toDateString().split(" ")[0]}</h5>
                        <p>
                          {new Date(dailyWeatherData.date).toDateString().split(" ")[2]}{" "}
                          {new Date(dailyWeatherData.date).toDateString().split(" ")[1]}{" "}
                        </p>
                        <p className="mb-0">
                          <FontAwesomeIcon icon={faTemperatureArrowUp} className="text-danger" />{" "}
                          {Math.round(dailyWeatherData.maxTemp)}°C
                        </p>
                        <p className="mb-0">
                          <FontAwesomeIcon icon={faTemperatureArrowDown} className="text-primary" />{" "}
                          {Math.round(dailyWeatherData.minTemp)}°C
                        </p>
                      </Col>
                      <Col className="d-flex flex-column justify-content-between align-items-end text-end">
                        <img
                          src={`${import.meta.env.BASE_URL}weather-icons/icon${dailyWeatherData.icon}.png`}
                          alt="weather icon"
                          width="48"
                        />
                        <p className="mb-0">
                          <FontAwesomeIcon icon={faDroplet} className="text-primary" /> {dailyWeatherData.rain}%
                        </p>
                      </Col>
                    </Row>
                  </div>
                ))}
              </div>
            </Col>
          </Row>
          <div className="text-center">
            <h5 className="mt-4">Viewing suggestions for this Hotspot</h5>
            <p>
              Data courtesy of the{" "}
              <a href="https://wwhandbook.iwc.int/en/responsible-management" target="_blank">
                Whale Watching Handbook
              </a>
            </p>
            <ul className="list-unstyled d-flex flex-wrap justify-content-center gap-3 pb-4">
              {hotspot.viewingSuggestions.map((suggestion) => (
                <li>
                  <Card style={{ width: "18rem" }}>
                    <Card.Img
                      className="bg-dark"
                      variant="top"
                      src={suggestion.species.exampleImageUrl}
                      alt="example image of a whale species "
                      style={{ height: "10rem", objectFit: "contain" }}
                    />
                    <Card.Body>
                      <Card.Title>{suggestion.species.name}</Card.Title>
                      Time of year: {suggestion.timeOfYear}
                      <br />
                      Platforms: {suggestion.platforms}
                      <br />
                      <a href={suggestion.species.wikiLink} target="_blank">
                        Find out more about {suggestion.species.name}s
                      </a>
                    </Card.Body>
                  </Card>
                </li>
              ))}
            </ul>
          </div>
        </Container>
      )}
      {loading && <p>Loading...</p>}
      {error && <p>Couldn't load the hotspot at this time</p>}
    </>
  )
}

export default HotspotView
