import axios from 'axios';
import { useState } from 'react';
import {Alert, Easing} from 'react-native';
import FormData from 'form-data';
import TextTicker from 'react-native-text-ticker';

import { devProxy, axiosConfig } from '../../utils/axiosConfig';
import { greetingList } from '../../utils/greetingList';

import StyledBtn from '../elements/Buttons/CustomButton';
import StyledSeparator from '../elements/Separator/Separator';
import { StyledTitle, StyledTitleItalic } from '../elements/typography';
import { StyledInput } from '../elements/inputs'

const Login = ({setAuthAction}) => {
    const [authData, setAuthData] = useState({username: '', psswd: ''}) 

    let greetingText = greetingList[Math.floor(Math.random()*greetingList.length)];

    const changeHandler = (value, name) => {
        setAuthData({...authData, [name]: value.nativeEvent.text})
     }

    const submitData = () => {
        const form = new FormData();
        form.append('UserNameOrEmail ', authData.username);
        form.append('Password ', authData.email);
        axios.post(devProxy + '/api/IdentityLogin', form, axiosConfig )
            .catch((error) => {
                if(error.response) {
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                }
                else if (error.request) {
                    console.log(error.request);
                }
                else {
                    console.log('Error ', error.message);
                }
            })
            .then((response) => {
                response.status === 200 ? setAuthAction('psswdConfirmation')
                : console.log('Not working sorry')
            })
     }

    return(
        <>
            <TextTicker
                style={{paddingBottom: 0}}
                scrollSpeed={16}
                loop
                repeatSpacer={10}
                easing={Easing.linear}
            >
                <StyledTitle>
                    {greetingText}
                </StyledTitle>
            </TextTicker>
            <TextTicker
                style={{paddingBottom: 0}}
                scrollSpeed={15}
                loop
                repeatSpacer={10}
                easing={Easing.linear}
            >
                <StyledTitleItalic>
                    {greetingText}
                </StyledTitleItalic>

            </TextTicker>
            <TextTicker
                style={{paddingBottom: 30}}
                scrollSpeed={17}
                loop
                repeatSpacer={10}
                easing={Easing.linear}
            >
                <StyledTitle>
                    {greetingText}
                </StyledTitle>
            </TextTicker>
            <StyledInput
                placeholder='Enter your email'
                onChange={value => changeHandler(value, 'username')}
            />
            <StyledInput
                secureTextEntry={true}
                placeholder='Enter your password'
                onChange={value => changeHandler(value, 'psswd')}
            />
            <StyledBtn 
                title="Login" 
                action={() => submitData()}
                type="primary"
            />
            <StyledBtn 
                p='0'
                halign='right'
                title="Forgot password?" 
                action={() => setAuthAction('psswdRecovery')}
                type="text"
            />
            <StyledSeparator text='Or Login with'/>
            <StyledBtn 
                disabled={true}
                iconColor='#518ef8'
                icon='logo-google'
                action={() => Alert.alert('Google login attempt')}
            />
            <StyledBtn 
                p='15px 0 0 0'
                color='#518ef8'
                halign='center'
                title="Don’t have an account? Register Now" 
                action={() => setAuthAction('registration')}
                type="text"
            />
        </>
    );
}
export default Login;