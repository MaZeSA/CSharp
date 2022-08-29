import { authReducer } from './../components/auth/store/reducer';
import { combineReducers } from "redux";
import { parentReducer } from '../components/kids/parent/store/reducer';

export const rootReducer = combineReducers({
    auth : authReducer,
    parent : parentReducer
});

export type RootState = ReturnType<typeof rootReducer>;