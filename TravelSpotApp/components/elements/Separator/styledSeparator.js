import styled from 'styled-components/native';
import { Text, View } from 'react-native';

export const StyledSeparatorWrapper = styled.View`
    max-width: 330px;
    width: 100%;
    margin: 30px 0 22px;
    display: flex;
    flex-direction: row;
    align-items: center;
`

export const StyledSeparatorContent = styled.Text`
    font-size: 14px;
    font-weight: 600;
    line-height: 14px;
    color: #6a707c;
    display: flex;
    padding: 0 12px;
`

export const StyledSeparatorLine = styled.View`
    height: 1px;
    flex: 1;
    display: flex;
    background: #6a707c;
    opacity: 0.5;
`