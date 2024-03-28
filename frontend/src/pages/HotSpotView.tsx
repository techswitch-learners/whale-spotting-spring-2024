import { useEffect, useState } from "react"
import { Card, Col, Container, Row } from "react-bootstrap"
import { useParams } from "react-router-dom"
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet"
import { Icon } from "leaflet"
import icon from "/favicon.ico"
import Weather from "../models/view/Weather"
import HotSpot from "../models/view/HotSpot"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faDroplet, faTemperatureArrowDown, faTemperatureArrowUp } from "@fortawesome/free-solid-svg-icons"

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
        <Container>
          <h1 className="text-center">{hotSpot.name}</h1>
          <h2 className="text-center mb-4">{hotSpot.country}</h2>
          <Row>
            <Col xs={12} md={4} className="d-flex flex-column align-items-center">
              <h5 className="text-center">Hotspot location</h5>
              <MapContainer
                style={{ height: "20rem", maxWidth: "100%", width: "32rem" }}
                center={[30, hotSpot.longitude]}
                zoom={1}
                scrollWheelZoom={true}
              >
                <TileLayer
                  attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                  url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                <Marker
                  key={hotSpot.id}
                  position={[Number(hotSpot.latitude), Number(hotSpot.longitude)]}
                  icon={customIcon}
                >
                  <Popup>{hotSpot.name}</Popup>
                </Marker>
              </MapContainer>
            </Col>
            <Col xs={12} md={8} className="d-flex flex-column align-items-center">
              {weather && (
                <>
                  <h5 className="text-center mt-3 mt-md-0">Weather forecast</h5>
                  <div className="d-flex flex-wrap justify-content-center gap-3">
                    {arr.map((x) => (
                      <div key={x.date} className="shadow p-3">
                        <Row style={{ width: "10rem" }}>
                          <Col className="text-start">
                            <h5 className="mb-0">{new Date(x.date).toDateString().split(" ")[0]}</h5>
                            <p>
                              {new Date(x.date).toDateString().split(" ")[2]}{" "}
                              {new Date(x.date).toDateString().split(" ")[1]}{" "}
                            </p>
                            <p className="mb-0">
                              <FontAwesomeIcon icon={faTemperatureArrowUp} className="text-danger" />{" "}
                              {Math.round(x.maxTemp)}°C
                            </p>
                            <p className="mb-0">
                              <FontAwesomeIcon icon={faTemperatureArrowDown} className="text-primary" />{" "}
                              {Math.round(x.minTemp)}°C
                            </p>
                          </Col>
                          <Col className="d-flex flex-column justify-content-between align-items-end text-end">
                            <img src={`${import.meta.env.BASE_URL}weather-icons/icon${x.icon}.png`} width="48" />
                            <p className="mb-0">
                              <FontAwesomeIcon icon={faDroplet} className="text-primary" /> {x.rain}%
                            </p>
                          </Col>
                        </Row>
                        {/* <p><img src={waterDrop} width="35"/> {x.rain}%</p> */}
                      </div>
                    ))}
                  </div>
                </>
              )}
              {loading && <p>Loading...</p>}
              {error && <p>Couldn't load weather data</p>}
            </Col>
          </Row>
          <h5 className="mt-4 text-center">Viewing suggestions for this hotspot</h5>
          <ul className="list-unstyled d-flex flex-wrap justify-content-center gap-3 pb-4 text-center">
            {hotSpot.viewingSuggestions.map((suggestion) => (
              <li>
                <Card style={{ width: "18rem" }}>
                  <Card.Img
                    className="bg-dark"
                    variant="top"
                    src={suggestion.species.exampleImageUrl}
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
        </Container>
      )}
    </>
  )
}

export default HotSpotView
