import { useEffect, useState, useRef } from 'react';

const Chatbot = () => {
    const [chatOutput, setChatOutput] = useState('');
    // Make abortRef type-safe: can be AbortController or null  
    const abortRef = useRef<AbortController | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            setChatOutput('');
            const ctrl = new AbortController();
            abortRef.current = ctrl;

            try {
                // Fetch event stream from backend API  
                const response = await fetch('https://localhost:XXXX/api/Chat', { // <-- Change this  
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'text/event-stream'
                    },
                    body: JSON.stringify({
                        chat_input: "USER QUERY",
                        chat_history: [
                            { inputs: { question: "Hello" }, outputs: { answer: "Hello! How can I assist you today?" } }
                        ]
                    }),
                    signal: ctrl.signal
                });

                if (!response.body) throw new Error("No stream body");

                const reader = response.body.getReader();
                const decoder = new TextDecoder('utf-8');
                let fullText = '';

                while (true) {
                    const { value, done } = await reader.read();
                    if (done) break;
                    const chunk = decoder.decode(value, { stream: true });
                    // Handle multiple SSE lines per chunk  
                    chunk.split('\n').forEach(line => {
                        if (line.startsWith('data: ')) {
                            const update = line.substring(6); // Remove "data: " //check for better way
                            fullText += update;
                            setChatOutput(fullText);
                        }
                    });
                }
            } catch (err) {
                if (err instanceof Error) {
                    if (err.name !== 'AbortError') setChatOutput('Streaming error: ' + err.message);
                    // ignore abort error  
                } else {
                    setChatOutput('Streaming error');
                }
            }
        };

        fetchData();

        // Cleanup: abort fetch on component unmount  
        return () => {
            if (abortRef.current) {
                abortRef.current.abort();
            }
        };
    }, []);

    return (
        <div style={{ padding: 16 }}>
            <h2>Chatbot (Streaming)</h2>
            <div style={{
                border: "1px solid #ccc",
                minHeight: 100,
                padding: 8,
                background: "#fafaff",
                whiteSpace: 'pre-wrap'
            }}>
                {chatOutput || 'Waiting for response...'}
            </div>
        </div>
    );
};

export default Chatbot;  