import {Alert} from 'react-native';
import { Title } from '../elements/typography';
import CustomBtn from '../elements/Buttons/CustomBtn';
import { StyledInput } from '../elements/inputs'
import Separator from '../elements/Separator/Separator';

import { registrationGreetingList } from '../../utils/greetingList';

const Registration = ({setAuthAction}) => {
    let greetingText = registrationGreetingList[Math.floor(Math.random()*registrationGreetingList.length)];

    return(
        <>
            <Title>
                {greetingText}
            </Title>
            <StyledInput
                placeholder='Username'
            />
            <StyledInput
                placeholder='Email'
            />
            <StyledInput
                placeholder='Password'
            />
            <StyledInput
                placeholder='Confirm password'
            />
            <CustomBtn 
                title="Login" 
                action={() => Alert.alert('Login attempt')}
                type="primary"
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
                title="Already have an account? Login Now" 
                action={() => setAuthAction('login')}
                type="text"
            />
        </>
    );
}
export default Registration;