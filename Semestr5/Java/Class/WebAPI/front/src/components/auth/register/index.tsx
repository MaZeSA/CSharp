import { GoogleReCaptchaProvider } from 'react-google-recaptcha-v3';
import RegisterPage from './registerPage';

const Register = () => {
	return (
		<>
		<GoogleReCaptchaProvider reCaptchaKey="6LcENgsiAAAAAICx1OkXn79Xx_9v4KhLtQAVBUv9">
			<RegisterPage />
		</GoogleReCaptchaProvider>
    </>
	)
}

export default Register;