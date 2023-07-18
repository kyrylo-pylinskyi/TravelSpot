import axios from 'axios';
import { useState } from 'react';
import { Alert } from 'react-native';
import FormData from 'form-data';

import { devProxy, axiosConfig } from '../../utils/axiosConfig';

import StyledBtn from '../elements/Buttons/CustomButton';
import StyledSeparator from '../elements/Separator/Separator';
import { StyledTitle } from '../elements/typography';
import { StyledInput } from '../elements/inputs'

const Login = ({setAuthAction, setCurrentPage}) => {
    const [authData, setAuthData] = useState({username: '', psswd: ''}) 

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
            <StyledTitle>
                Login
            </StyledTitle>
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
                iconColor='#518EF8'
                icon='logo-google'
                action={() => Alert.alert('Google login attempt')}
            />
            <StyledBtn 
                p='15px 0 0 0'
                color='#518EF8'
                halign='center'
                title="Don’t have an account? Register Now" 
                action={() => setAuthAction('registration')}
                type="text"
            />
        </>
    );
}
export default Login;