import styled from 'styled-components/native';
import { Text } from 'react-native';


export const StyledTitle = styled.Text`
    padding: ${props => props.p ? props.p : '0'};
    max-width: 330px;
    width: 100%;
    margin-bottom: 10px;
    font-size: 30px;
    color: #1E232C;
    font-family: 'Urbanist_700Bold';
`

export const StyledText = styled.Text`
    max-width: 330px;
    width: 100%;
    margin-bottom: 10px;
    font-size: 16px;
    color: #8391A1;
    font-weight: 500;
    font-family: 'Urbanist_500Medium';
`