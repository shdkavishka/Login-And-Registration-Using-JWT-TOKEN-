import React, { useState } from 'react';
import axios from 'axios';

const Register = () => {
    const [Email, setEmail] = useState('');
    const [PasswordHash, setPassword] = useState('');
   // const [passwordhash, setPhash] = useState('');

    const handleRegistration = async (e) => {
        e.preventDefault();
        
        try {
            const response = await axios.post('http://localhost:5212/api/Registration', {
                Email,
                PasswordHash
               // passwordhash,
            });

            console.log(response.data);
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <form onSubmit={handleRegistration}>
            <input
                type="email"
                placeholder="Email"
                value={Email}
                onChange={(e) => setEmail(e.target.value)}
            />
            <input
                type="password"
                placeholder="Password"
                value={PasswordHash}
                onChange={(e) => setPassword(e.target.value)}
            />
          {/*  <input
                type="passwordHash"
                placeholder="PasswordHash"
                value={passwordhash}
                onChange={(e) => setPhash(e.target.value)}
            />*/}

            <button type="submit">Register</button>
        </form>
    );
};

export default Register;