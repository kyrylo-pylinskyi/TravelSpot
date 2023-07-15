import {Alert} from 'react-native';
import { Title } from '../elements/typography';
import CustomBtn from '../elements/Buttons/CustomBtn';
import { StyledInput } from '../elements/inputs'

import { greetingList } from '../../utils/greetingList';
import Separator from '../elements/Separator/Separator';

const Login = ({setAuthAction}) => {
    let greetingText = greetingList[Math.floor(Math.random()*greetingList.length)];

    return(
        <>
            <Title>
                {greetingText}
            </Title>
            <StyledInput
                placeholder='Enter your username'
            />
            <StyledInput
                placeholder='Enter your password'
            />
            <CustomBtn 
                title="Login" 
                action={() => Alert.alert('Login attempt')}
                type="primary"
            />
            <CustomBtn 
                p='0'
                halign='right'
                title="Forgot password?" 
                action={() => Alert.alert('Login attempt')}
                type="text"
            />
            <Separator text='Or login with'/>
            <CustomBtn 
                disabled={true}
                iconColor='#518ef8'
                icon='google'
                action={() => Alert.alert('Google login attempt')}
            />
            <CustomBtn 
                p='0'
                halign='center'
                title="Don’t have an account? Register Now" 
                action={() => setAuthAction('registration')}
                type="text"
            />
        </>
    );
}
export default Login;