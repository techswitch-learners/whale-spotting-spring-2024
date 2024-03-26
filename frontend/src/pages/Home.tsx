import { MouseEventHandler, useContext, useEffect, useRef, useState } from "react"
import { BackgroundContext } from "../App"
import whaleAnimation from "../assets/whale-animation.gif"
import { Button } from "react-bootstrap"
import { LinkContainer } from "react-router-bootstrap"
import NavbarBottom from "../components/NavbarBottom"
// import eye from "../assets/eye.svg"

const Home = () => {
  const backgroundContext = useContext(BackgroundContext)
  const eyeRef = useRef<SVGCircleElement>(null)
  const [eyePosition, setEyePosition] = useState({ x: 0, y: 0 })

  useEffect(() => {
    backgroundContext.setBackground(`url("${whaleAnimation}")`)
  }, [backgroundContext])

  useEffect(() => {
    const eye = eyeRef.current
    if (eye) {
      const eyeRect = eye.getBBox()
      const eyeCenterX = eyeRect.x + eyeRect.width / 2
      const eyeCenterY = eyeRect.y + eyeRect.height / 2
      setEyePosition({ x: eyeCenterX, y: eyeCenterY })
    }
  }, [])

  function trackMouseMove(event: React.MouseEvent<SVGSVGElement, MouseEvent>) {
    const { clientX, clientY } = event
    const eyeCenterX = eyePosition.x
    const eyeCenterY = eyePosition.y

    if (eyeRef.current) {
      const deltaX = clientX - eyeCenterX
      const deltaY = clientY - eyeCenterY
      const angle = (Math.atan2(deltaY, deltaX) * 180) / Math.PI
      const distance = Math.min(eyeRef.current.getBBox().width / 3, Math.sqrt(deltaX ** 2 + deltaY ** 2))

      const iris = document.getElementById("iris")
      iris!.style.transform = `translate(${distance * Math.cos(angle)}px, ${distance * Math.sin(angle)}px)`

      const pupil = document.getElementById("pupil")
      pupil!.style.transform = `translate(${distance * Math.cos(angle)}px, ${distance * Math.sin(angle)}px)`
    }
  }
  return (
    <>
      <div className="Home row min-h-100" onMouseMove={trackMouseMove as MouseEventHandler}>
        <div className="col-lg d-flex flex-column justify-content-center pe-lg-5">
          <h1 className="display-3 text-center text-white" aria-label="Welcome Whale Spotters!">
            Welcome
            <br />
            Whale
            <br />
            Sp
            <svg width="40" height="40" viewBox="0 0 50 50" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="25" cy="25" r="20" stroke="black" strokeWidth="2" fill="white" ref={eyeRef} />
              <circle cx="25" cy="25" r="10" fill="#90CAF9" id="iris" />
              <circle cx="25" cy="25" r="5" fill="black" id="pupil" />
              {/* <!-- Eyelashes --> */}
              <line x1="15" y1="10" x2="10" y2="2" stroke="black" strokeWidth="2" />
              <line x1="20" y1="8" x2="15" y2="0" stroke="black" strokeWidth="2" />
              <line x1="25" y1="7" x2="25" y2="-1" stroke="black" strokeWidth="2" />
              <line x1="30" y1="8" x2="35" y2="0" stroke="black" strokeWidth="2" />
              <line x1="35" y1="10" x2="40" y2="2" stroke="black" strokeWidth="2" />
            </svg>
            {/* <img src={eye} id="eyeId" alt="Eye taking the place of letter O" /> */}
            tters!
          </h1>
        </div>
        <div className="col-lg d-flex flex-column justify-content-center">
          <div className="Home-button-container d-flex flex-column justify-content-center align-items-stretch gap-3">
            <LinkContainer to="/sightings/add">
              <Button variant="translucent" size="lg">
                Add a whale sighting
              </Button>
            </LinkContainer>
            <LinkContainer to="/sightings">
              <Button variant="translucent" size="lg">
                See whale sightings
              </Button>
            </LinkContainer>
            <Button variant="translucent" size="lg">
              Check the weather
            </Button>
          </div>
          <div className="Home-button-container d-flex flex-column justify-content-center align-items-stretch gap-3">
            <LinkContainer to="/sightings/add">
              <Button variant="translucent">Add a whale sighting</Button>
            </LinkContainer>
            <LinkContainer to="/sightings">
              <Button variant="translucent">See whale sightings</Button>
            </LinkContainer>
            <Button variant="translucent">Check the weather</Button>
          </div>
        </div>
      </div>
      <NavbarBottom />
    </>
  )
}
export default Home
