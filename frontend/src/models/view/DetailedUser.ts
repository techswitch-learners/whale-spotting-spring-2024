interface AdminUser {
  id: number
  profileImageUrl: string
  userName: string
  email: string | null
  phoneNumber: string | null
  accessFailedCount: number
}

export default AdminUser
