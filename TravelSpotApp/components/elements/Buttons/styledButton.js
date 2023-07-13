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
                color: white;

            `
        : props.type === 'secondary' ? 
            `
                background: blue;
                color: white;
            `
        : props.type === 'text' ? 
            `
                background: transparent;
                color: blue;
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
    font-weight: 600;
    ${props => 
        props.type === 'primary' ? 
            `
                color: white;
            `
        : props.type === 'secondary' ? 
            `
                color: white;
            `
        : props.type === 'text' ? 
            `
                font-weight: 600;
                font-size: 14px;
                color: #6a707c;
            `
        :
            `
                color: #1e232c;
            `
    }

`;