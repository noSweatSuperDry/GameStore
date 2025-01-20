import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css';

const App = () => {
  const [games, setGames] = useState([]);
  const [genres, setGenres] = useState([]);
  const [newGame, setNewGame] = useState({ name: '', genreId: '', price: '', releaseDate: '' });
  const [editGame, setEditGame] = useState(null);

  // Fetch all games and genres
  useEffect(() => {
    fetchGames();
    fetchGenres();
  }, []);

  const fetchGames = async () => {
    try {
      const response = await axios.get('http://localhost:5232/games');
      setGames(response.data);
    } catch (error) {
      console.error('Error fetching games:', error);
    }
  };

  const fetchGenres = async () => {
    try {
      const response = await axios.get('http://localhost:5232/genres');
      setGenres(response.data);
    } catch (error) {
      console.error('Error fetching genres:', error);
    }
  };

  // Add a new game
  const handleAddGame = async () => {
    try {
      await axios.post('http://localhost:5232/games', newGame);
      setNewGame({ name: '', genreId: '', price: '', releaseDate: '' });
      fetchGames();
    } catch (error) {
      console.error('Error adding game:', error);
    }
  };

  // Update an existing game
  const handleUpdateGame = async () => {
    try {
      await axios.put(`http://localhost:5232/games/${editGame.id}`, editGame);
      setEditGame(null);
      fetchGames();
    } catch (error) {
      console.error('Error updating game:', error);
    }
  };

  // Delete a game
  const handleDeleteGame = async (id) => {
    try {
      await axios.delete(`http://localhost:5232/games/${id}`);
      fetchGames();
    } catch (error) {
      console.error('Error deleting game:', error);
    }
  };

  return (
    <div className="app-container">
      <h1>Game Management</h1>

      {/* Add New Game */}
      <div className="form-container">
        <h2>Add New Game</h2>
        <input
          type="text"
          placeholder="Name"
          value={newGame.name}
          onChange={(e) => setNewGame({ ...newGame, name: e.target.value })}
        />
        <select
          value={newGame.genreId}
          onChange={(e) => setNewGame({ ...newGame, genreId: e.target.value })}
        >
          <option value="">Select Genre</option>
          {genres.map((genre) => (
            <option key={genre.id} value={genre.id}>
              {genre.name}
            </option>
          ))}
        </select>
        <input
          type="number"
          placeholder="Price"
          value={newGame.price}
          onChange={(e) => setNewGame({ ...newGame, price: e.target.value })}
        />
        <input
          type="date"
          value={newGame.releaseDate}
          onChange={(e) => setNewGame({ ...newGame, releaseDate: e.target.value })}
        />
        <button className="add-button" onClick={handleAddGame}>Add Game</button>
      </div>

      {/* Edit Game */}
      {editGame && (
        <div className="form-container">
          <h2>Edit Game</h2>
          <input
            type="text"
            placeholder="Name"
            value={editGame.name}
            onChange={(e) => setEditGame({ ...editGame, name: e.target.value })}
          />
          <select
            value={editGame.genreId}
            onChange={(e) => setEditGame({ ...editGame, genreId: e.target.value })}
          >
            <option value="">Select Genre</option>
            {genres.map((genre) => (
              <option key={genre.id} value={genre.id}>
                {genre.name}
              </option>
            ))}
          </select>
          <input
            type="number"
            placeholder="Price"
            value={editGame.price}
            onChange={(e) => setEditGame({ ...editGame, price: e.target.value })}
          />
          <input
            type="date"
            value={editGame.releaseDate}
            onChange={(e) => setEditGame({ ...editGame, releaseDate: e.target.value })}
          />
          <button className="update-button" onClick={handleUpdateGame}>Update Game</button>
          <button className="cancel-button" onClick={() => setEditGame(null)}>Cancel</button>
        </div>
      )}

      {/* Game List */}
      <div className="table-container">
        <h2>Game List</h2>
        <table className="game-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Genre</th>
              <th>Price</th>
              <th>Release Date</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {games.map((game) => (
              <tr key={game.id}>
                <td>{game.id}</td>
                <td>{game.name}</td>
                <td>{genres.find((genre) => genre.id === game.genreId)?.name || 'Unknown'}</td>
                <td>${game.price.toFixed(2)}</td>
                <td>{game.releaseDate}</td>
                <td>
                  <button className="edit-button" onClick={() => setEditGame(game)}>Edit</button>
                  <button className="delete-button" onClick={() => handleDeleteGame(game.id)}>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default App;