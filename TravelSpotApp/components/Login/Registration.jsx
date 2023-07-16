import axios from 'axios';
import { useState } from 'react';
import {Alert} from 'react-native';

import CustomBtn from '../elements/Buttons/CustomBtn';
import Separator from '../elements/Separator/Separator';
import { Title } from '../elements/typography';
import { StyledInput } from '../elements/inputs'

import { registrationGreetingList } from '../../utils/greetingList';

const Registration = ({setAuthAction}) => {
    let greetingText = registrationGreetingList[Math.floor(Math.random()*registrationGreetingList.length)];
    const [authData, setAuthData] = useState({username: '', email: '', psswd: '', confirmPsswd: ''}) 

    const changeHandler = (value, name) => {
        setAuthData({...authData, [name]: value.nativeEvent.text})
     }

     const submitData = () => {
        axios.post('https://localhost:7018/api/Registration/register', {Email: authData.email, Name: authData.username, Password: authData.psswd, PasswordConfirm: authData.confirmPsswd})
        .then(Response => console.log(Response))
        .catch(error=>console.log(error))
     }

    return(
        <>
            <Title>
                {greetingText}
            </Title>
            <StyledInput
                placeholder='Username'
                onChange={value => changeHandler(value, 'username')}
            />
            <StyledInput
                placeholder='Email'
                onChange={value => changeHandler(value, 'email')}
                inputMode='email'
            />
            <StyledInput
                secureTextEntry={true}
                placeholder='Password'
                onChange={value => changeHandler(value, 'psswd')}
            />
            <StyledInput
                secureTextEntry={true}
                placeholder='Confirm password'
                onChange={value => changeHandler(value, 'confirmPsswd')}
            />
            <CustomBtn 
                title="Login" 
                action={() => submitData()}
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
                p='15px 0 0 0'
                color='#518ef8'
                halign='center'
                title="Already have an account? Login Now" 
                action={() => setAuthAction('login')}
                type="text"
            />
        </>
    );
}
export default Registration;