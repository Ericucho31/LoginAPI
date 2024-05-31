// src/pages/XSSExamples.js

import React, { useState } from 'react';

const XSSExamples = () => {
  const [input, setInput] = useState('');

  const [stored, setStored] = useState('');
  const [reflected, setReflected] = useState('');


  const handleInputChange = (e) => {
    setInput(e.target.value);
  };

  // Example 1: Stored XSS
  const handleStoredXSS = () => {
    localStorage.setItem('storedXSS', input);
    setStored(localStorage.getItem('storedXSS'));
  };

  // Example 2: Reflected XSS
  const handleReflectedXSS = (e) => {
    e.preventDefault();
    setReflected(input);
  };

  // Example 3: DOM-based XSS
  const handleDOMBasedXSS = () => {
    document.getElementById('domOutput').innerHTML = input;
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">XSS Examples</h1>

      <div className="mb-4">
        <h2 className="text-xl font-semibold mb-2">Stored XSS</h2>
        <input
          type="text"
          className="border p-2 w-full mb-2"
          placeholder="Enter input for Stored XSS"
          value={input}
          onChange={handleInputChange}
        />
        <button
          className="bg-blue-500 text-white p-2"
          onClick={handleStoredXSS}
        >
          Store and Display
        </button>
        <div
          className="mt-2 p-2 border bg-gray-100"
          dangerouslySetInnerHTML={{ __html: stored }}
        ></div>
      </div>

      <div className="mb-4">
        <h2 className="text-xl font-semibold mb-2">Reflected XSS</h2>
        <form onSubmit={handleReflectedXSS}>
          <input
            type="text"
            className="border p-2 w-full mb-2"
            placeholder="Enter input for Reflected XSS"
            value={input}
            onChange={handleInputChange}
          />
          <button
            type="submit"
            className="bg-blue-500 text-white p-2"
          >
            Reflect and Display
          </button>
        </form>
        <div
          className="mt-2 p-2 border bg-gray-100"
          dangerouslySetInnerHTML={{ __html: reflected }}
        ></div>
      </div>

      <div className="mb-4">
        <h2 className="text-xl font-semibold mb-2">DOM-based XSS</h2>
        <input
          type="text"
          className="border p-2 w-full mb-2"
          placeholder="Enter input for DOM-based XSS"
          value={input}
          onChange={handleInputChange}
        />
        <button
          className="bg-blue-500 text-white p-2"
          onClick={handleDOMBasedXSS}
        >
          Inject into DOM
        </button>
        <div
          id="domOutput"
          className="mt-2 p-2 border bg-gray-100"
        ></div>
      </div>
    </div>
  );
};

export default XSSExamples;
