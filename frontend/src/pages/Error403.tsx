import { useContext, useEffect } from "react"
import { BackgroundContext } from "../App"
import divingWhale from "../assets/diving-whale.gif"
import NavbarBottom from "../components/NavbarBottom"

const Error403 = () => {
  const backgroundContext = useContext(BackgroundContext)

  useEffect(() => {
    backgroundContext.setBackground("linear-gradient(to bottom, white, #5694bf 85%, #135d9b)")
  }, [backgroundContext])

  return (
    <>
      <div className="d-flex flex-column justify-content-center flex-grow-1">
        <h1 className="text-center" style={{ color: "#135d9b" }}>
          Error 403
        </h1>
        <h2 className="text-center fw-normal mb-0" style={{ color: "#135d9b" }}>
          Whale not allowed
        </h2>
        <img src={divingWhale} className="mx-auto" style={{ width: "20rem", height: "20rem" }} />
      </div>
      <NavbarBottom />
    </>
  )
}
export default Error403
