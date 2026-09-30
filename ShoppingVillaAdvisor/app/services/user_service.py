from app.repositories.user_repository import UserRepository
from app.db.models.user import User
from app.schemas.user import UserCreate, UserResponse

class UserService:
    def __init__(self, user_repository: UserRepository):
        self.user_repository = user_repository

    def get_user(self, user_id: int) -> UserResponse:
        user = self.user_repository.get_user(user_id)
        if user:
            return UserResponse.from_orm(user)
        return None

    def create_user(self, user_create: UserCreate) -> UserResponse:
        user = User(
            username=user_create.username,
            email=user_create.email,
            password=user_create.password
        )
        user = self.user_repository.create_user(user)
        return UserResponse.from_orm(user)

    def get_user_by_email(self, email: str) -> UserResponse:
        user = self.user_repository.get_user_by_email(email)
        if user:
            return UserResponse.from_orm(user)
        return None

    def get_user_by_username(self, username: str) -> UserResponse:
        user = self.user_repository.get_user_by_username(username)
        if user:
            return UserResponse.from_orm(user)
        return None