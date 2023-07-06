import React, { useState } from 'react';
import axios from 'axios';

const LoginForm = () => {
    const [Email, setEmail] = useState('');
    const [PasswordHash, setPassword] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();
       
        try {
            const response = await axios.post('http://localhost:5212/api/Login', {
                Email,
                PasswordHash,
            });

            console.log(response.data);
            // Store the JWT token in local storage or cookies for subsequent API requests
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <form onSubmit={handleLogin}>
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
            <button type="submit">Login</button>
        </form>
    );
};

export default LoginForm;
