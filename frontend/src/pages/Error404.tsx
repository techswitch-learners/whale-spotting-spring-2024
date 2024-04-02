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
      <div className="Home min-h-100 d-flex flex-column justify-content-center align-items-center">
        <h1 className="text-center mb-5" style={{ color: "#e052ac" }}>
          404: Whale not found
        </h1>
        <img src={flamingo} style={{ width: "22.5rem", height: "22.5rem" }} />
      </div>
      <NavbarBottom />
    </>
  )
}
export default Error404
