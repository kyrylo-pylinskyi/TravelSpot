import styled from 'styled-components/native';
import { Pressable, Text } from 'react-native';

export const ButtonWrapper = styled.Pressable`
    max-width: 330px;
    width: 100%;
    padding: ${props => props.p ? props.p : props.icon ? '14px 0' : '19px 0'};
    margin-bottom: 10px;
    display: flex;
    align-items: ${props => 
        props.halign === 'left' ? 'flex-start' 
        : props.halign === 'right' ? 'flex-end'
        : props.halign === 'center' ? 'center'
        : 'center'
    };
    border-radius: 8px;
    ${props => 
        props.type === 'primary' ? 
            `
                background: #1e232c;

            `
        : props.type === 'secondary' ? 
            `
                background: #FFFFFF;
                border: 1px solid #1E232C;
            `
        : props.type === 'text' ? 
            `
                background: transparent;
            `
        : props.type === 'nav' ? 
            `
                width: 40px;
                height: 40px;
                padding: 6px;
                background: transparent;
                border: 1px solid #E8ECF4;
                border-radius: 12px;
            `
        : props.disabled === true ? 
            `
                background: rgba(106, 112, 124, 0.5);
                opacity: 0.5;
            `
        :
            `
                background: #ffffff;
                color: #1e232c;
                border: 1px solid #dadada;
            `
    }
    `;

export const ButtonText = styled.Text`
    font-size: 15px;
    font-family: 'Urbanist_500Medium';
    color: ${props => props.color ? props.color : '#6a707c'};
    ${props => 
        props.type === 'primary' ? 
            `
                color: #FFFFFF;
            `
        : props.type === 'secondary' ? 
            `
                color: #1E232C;
            `
        : props.type === 'text' ? 
            `
                font-weight: 600;
                font-size: 14px;
            `
        :
            `
                color: #1e232c;
            `
    }

`;