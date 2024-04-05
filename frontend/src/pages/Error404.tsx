import { useContext, useEffect } from "react"
import { BackgroundContext } from "../App"
import flamingo from "../assets/flamingo.gif"
import NavbarBottom from "../components/NavbarBottom"

const Error404 = () => {
  const backgroundContext = useContext(BackgroundContext)

  useEffect(() => {
    backgroundContext.setBackground("linear-gradient(to bottom, white, #f69bd5 85%, #ff6ac9)")
  }, [backgroundContext])

  return (
    <>
      <div className="d-flex flex-column justify-content-center flex-grow-1">
        <h1 className="text-center" style={{ color: "#e052ac" }}>
          Error 404
        </h1>
        <h2 className="text-center mb-5 fw-normal" style={{ color: "#e052ac" }}>
          Whale not found
        </h2>
        <img
          src={flamingo}
          alt="animated image of three of pink flamingos"
          className="mx-auto"
          style={{ width: "22.5rem", height: "22.5rem" }}
        />
      </div>
      <NavbarBottom />
    </>
  )
}
export default Error404
