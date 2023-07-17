import axios from 'axios';
import { useState } from 'react';
import {Alert} from 'react-native';
import FormData from 'form-data';

import { devProxy, axiosConfig } from '../../utils/axiosConfig';

import CustomBtn from '../elements/Buttons/CustomBtn';
import Separator from '../elements/Separator/Separator';
import { Title } from '../elements/typography';
import { StyledInput } from '../elements/inputs'

const Registration = ({setAuthAction}) => {
    const [authData, setAuthData] = useState({username: '', email: '', psswd: '', confirmPsswd: ''}) 

    const changeHandler = (value, name) => {
        setAuthData({...authData, [name]: value.nativeEvent.text})
     }

     const submitData = () => {
        const form = new FormData();
        form.append('Name', authData.username);
        form.append('Email', authData.email);
        form.append('Password', authData.psswd);
        form.append('PasswordConfirm', authData.confirmPsswd);
        axios.post(devProxy + '/api/IdentityRegistration', form, axiosConfig )
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
     }

    return(
        <>
            <Title>
                Registration
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
                title="Register" 
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