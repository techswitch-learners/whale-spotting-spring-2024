import { useContext, useEffect } from "react"
import { BackgroundContext } from "../App"
import whaleAnimation from "../assets/whale-animation.gif"
import eye from "../assets/eye.svg"
import { Button } from "react-bootstrap"

const Home = () => {
  const backgroundContext = useContext(BackgroundContext)

  useEffect(() => {
    backgroundContext.setBackground(`url("${whaleAnimation}")`)
  }, [backgroundContext])

  return (
    <div className="Home row min-h-100">
      <div className="col-lg d-flex flex-column justify-content-center pe-lg-5">
        <h1 className="display-3 text-center text-white">
          Welcome
          <br />
          Whale
          <br />
          Sp<img src={eye} id="eyeId"></img>tters!
        </h1>
      </div>
      <div className="col-lg d-flex flex-column justify-content-center">
        <div className="Home-button-container d-flex flex-column justify-content-center align-items-stretch gap-3">
          <Button variant="translucent" size="lg">
            Add a whale sighting
          </Button>
          <Button variant="translucent" size="lg">
            See whale sightings
          </Button>
          <Button variant="translucent" size="lg">
            Check the weather
          </Button>
        </div>
        <div className="Home-button-container d-flex flex-column justify-content-center align-items-stretch gap-3">
          <Button variant="translucent">Add a whale sighting</Button>
          <Button variant="translucent">See whale sightings</Button>
          <Button variant="translucent">Check the weather</Button>
        </div>
      </div>
    </div>
  )
}
export default Home
