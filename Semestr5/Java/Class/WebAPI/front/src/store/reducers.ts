import { combineReducers } from "redux";
import { authReducer } from "../components/auth/login/reducer";
import { registerReducer } from "../components/auth/register/reducer";
import { parentReducer } from '../components/kids/parent/store/reducer';

export const rootReducer = combineReducers({
    auth : authReducer,
    register : registerReducer,
    parent : parentReducer

});

export type RootState = ReturnType<typeof rootReducer>;