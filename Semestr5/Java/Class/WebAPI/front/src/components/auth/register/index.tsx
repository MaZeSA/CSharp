import { GoogleReCaptchaProvider } from 'react-google-recaptcha-v3';
import RegisterPage from "./register";
const Register = () => {
	return (
		<>
		<GoogleReCaptchaProvider reCaptchaKey="6LdXS7cdAAAAAKpzlFA5oHnaB8tPUF2ZtAIqVCRc">
			<RegisterPage />
		</GoogleReCaptchaProvider>
		
		</>
	)
}

export default Register;