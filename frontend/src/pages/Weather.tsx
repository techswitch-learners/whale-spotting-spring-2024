import { useEffect, useState } from "react"
import { Col, Container, Row } from "react-bootstrap"
import { useParams } from "react-router-dom"

interface Weather {
  latitude: number
  longitude: number
  generationtime_ms: number
  utc_offset_seconds: number
  timezone: string
  timezone_abbreviation: string
  elevation: number
  current_units: {
    time: string
    interval: string
    temperature_2m: string
    relative_humidity_2m: string
    weather_code: string
    precipitation: string
  }
  current: {
    time: string
    interval: number
    temperature_2m: number
    relative_humidity_2m: number
    apparent_temperature: number
    weather_code: number
    precipitation: number
  }
  daily_units: {
    time: string
    weather_code: string
    temperature_2m_max: string
    temperature_2m_min: string
    precipitation_sum: string
    precipitation_probability_max: string
  }
  daily: {
    time: [string, string, string, string, string, string, string]
    weather_code: [number, number, number, number, number, number, number]
    temperature_2m_max: [number, number, number, number, number, number, number]
    temperature_2m_min: [number, number, number, number, number, number, number]
    precipitation_sum: [number, number, number, number, number, number, number]
    precipitation_probability_max: [number, number, number, number, number, number, number]
  }
}

const hotSpots = [
  {
    hotSpotName: "Puerto Piramides",
    coordinates: {
      lat: "-42.566667",
      lon: "-64.283333",
    },
  },
  {
    hotSpotName: "Victoria",
    coordinates: {
      lat: "48.428333",
      lon: "-123.364722",
    },
  },
  {
    hotSpotName: "Dulce Gulf",
    coordinates: {
      lat: "8.583333",
      lon: "-83.266667",
    },
  },
]

function Weather() {
  const { id } = useParams()
  const [weather, setWeather] = useState<Weather>()
  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<boolean>(false)

  const handleSubmit = () => {
    setWeather(undefined)
    setError(false)

    const lat = hotSpots[parseInt(id!)].coordinates.lat
    const lon = hotSpots[parseInt(id!)].coordinates.lon

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

  useEffect(handleSubmit, [id])

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

  return (
    <main className="d-flex align-items-center justify-content-center text-center">
      {weather && (
        <div>
          <br />
          <h5>Current weather:</h5>
          <img src={`${import.meta.env.BASE_URL}weather-icons/icon${weather.current.weather_code}.png`} width="35" />
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
  )
}

export default Weather
