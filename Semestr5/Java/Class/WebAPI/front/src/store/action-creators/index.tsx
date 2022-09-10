import * as AuthActionCreators from "../../components/auth/login/actions";
import * as RegActionCreators from "../../components/auth/register/actions";
import * as ParentActionCreator from "../../components/kids/parent/store/actions"

const actions = {
  ...AuthActionCreators,
  ...RegActionCreators,
  ...ParentActionCreator
};

export default actions;
