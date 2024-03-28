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

export default Weather
