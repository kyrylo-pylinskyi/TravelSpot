import { useState } from 'react';
import AuthPage from './screens/AuthPage/AuthPage';
import { MainView } from './components/style';
import { 
  useFonts, 
  Urbanist_400Regular, 
  Urbanist_500Medium, 
  Urbanist_600SemiBold, 
  Urbanist_700Bold, 
  Urbanist_600SemiBold_Italic,
  Urbanist_700Bold_Italic
} from '@expo-google-fonts/urbanist'
import GreetingPage from './screens/GreetingPage/GreetingPage';

export default function App() {
  const [currentPage, setCurrentPage] = useState('greeting');

  let [fontsLoaded] = useFonts({
    Urbanist_400Regular,
    Urbanist_500Medium,
    Urbanist_600SemiBold,
    Urbanist_700Bold,
    Urbanist_600SemiBold_Italic,
    Urbanist_700Bold_Italic
  });

  if (!fontsLoaded) {
    return null;
  }

  return (
    <MainView>
      {
        currentPage === 'greeting' ? <GreetingPage setCurrentPage={setCurrentPage}/>
        : currentPage === 'login' || currentPage === 'registration' ? <AuthPage basicAction={currentPage} setCurrentPage={setCurrentPage} />
        : <></>
      }
    </MainView>
    );
}