import { useContext, useState } from "react"
import { Button, Stack } from "react-bootstrap"
import { addReaction, updateReaction, deleteReaction } from "../api/backendClient"
import { AuthContext } from "../App"
import { useNavigate } from "react-router-dom"
import ReactionType from "../types/ReactionType"
import Reaction from "../models/view/Reaction"

const emojis = {
  LetsParty: "🤩",
  NiceCatch: "😊",
  SoSo: "😐",
  Suspicious: "🤔",
}
interface ReactionProps {
  reactions: Reaction
  currentUserReaction: string | null
  sightingId: number
}

const Reactions = ({ reactions, currentUserReaction, sightingId }: ReactionProps) => {
  // const reactions= {
  //   "LetsParty":3,
  //   "NiceCatch": 1,
  //   "SoSo":4,
  //   "Suspicious":5
  // }
  // const sightingId = 1
  // let currentUserReaction :null= null;

  const authContext = useContext(AuthContext)
  const navigate = useNavigate()

  //const [currentUserReaction, setcurrentUserReaction] = useState<string>('')
  const [, setInputUserReaction] = useState<ReactionType | null>(null)
  const [, setErrors] = useState<{ [subject: string]: string[] }>({})

  const handleClick = (value: ReactionType) => {
    console.log(`current user reaction: ${currentUserReaction}`)
    console.log(`value: ${value}`)
    if (currentUserReaction === value) {
      deleteReaction(sightingId, authContext.cookie.token)
        .then((response) => {
          if (response.ok) {
            setInputUserReaction(null)
          } else if (response.status === 401) {
            authContext.removeCookie("token")
            navigate("/login")
          } else {
            response.json().then((content) => {
              setErrors(content.errors)
            })
          }
        })
        .catch(() => setErrors({ General: ["Unable to delete your reaction"] }))
    } else if (value !== currentUserReaction && currentUserReaction !== null) {
      updateReaction({ reactionType: value, sightingId: sightingId }, authContext.cookie.token)
        .then((response) => {
          if (response.ok) {
            setInputUserReaction(value)
          } else if (response.status === 401) {
            authContext.removeCookie("token")
            navigate("/login")
          } else {
            response.json().then((content) => {
              setErrors(content.errors)
            })
          }
        })
        .catch(() => setErrors({ General: ["Unable to update reaction"] }))
    } else {
      addReaction({ reactionType: value, sightingId: sightingId }, authContext.cookie.token)
        .then((response) => {
          if (response.ok) {
            setInputUserReaction(value)
          } else if (response.status === 401) {
            authContext.removeCookie("token")
            navigate("/login")
          } else {
            response.json().then((content) => {
              setErrors(content.errors)
            })
          }
        })
        .catch(() => setErrors({ General: ["Unable to add reaction"] }))
      //.finally(() => setLoading(false))
    }
  }

  return (
    <>
      <Stack direction="horizontal">
        {Object.entries(emojis).map(([key, emoji]) => (
          <div key={key}>
            <Button variant="light" onClick={() => handleClick(key as ReactionType)}>
              {emoji}
            </Button>
            <span style={{ position: "relative", top: "0.75rem" }}>{reactions[key as ReactionType]}</span>
          </div>
        ))}
      </Stack>
    </>
  )
}

export default Reactions
