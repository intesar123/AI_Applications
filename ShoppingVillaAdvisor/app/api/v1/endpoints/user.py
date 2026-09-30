from app.schemas.user import UserCreate, UserResponse
from app.services.user_service import UserService
from app.repositories.user_repository import UserRepository
from app.db.session import SessionLocal
from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session

router = APIRouter()

def get_db():
    db = None
    try:
        db = SessionLocal()
        yield db
    finally:
        if db:
            db.close()

def get_user_service(db: Session = Depends(get_db)) -> UserService:
    return UserService(UserRepository(db))

@router.post("/users/", response_model=UserResponse)
def create_user(user_create: UserCreate, service: UserService = Depends(get_user_service)):
    return service.create_user(user_create)

# add more user-related endpoints here

@router.get("/users/{user_id}", response_model=UserResponse)
def get_user(user_id: int, service: UserService = Depends(get_user_service)):
    return service.get_user(user_id)

@router.delete("/users/{user_id}", response_model=UserResponse)
def delete_user(user_id: int, service: UserService = Depends(get_user_service)):
    return service.delete_user(user_id)

@router.put("/users/{user_id}", response_model=UserResponse)
def update_user(user_id: int, user_update: UserCreate, service: UserService = Depends(get_user_service)):
    return service.update_user(user_id, user_update)