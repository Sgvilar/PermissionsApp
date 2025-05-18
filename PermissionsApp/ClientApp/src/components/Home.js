import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
    Box,
    TextField,
    MenuItem,
    Button,
    Typography,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper
} from '@mui/material';

const API_BASE = 'https://localhost:44334';

const Home = () => {
    const [permissions, setPermissions] = useState([]);
    const [permissionTypes, setPermissionTypes] = useState([]);

    const [form, setForm] = useState({
        employeeFirstName: '',
        employeeLastName: '',
        permissionTypeId: '',
        permissionDate: new Date().toISOString().split('T')[0],
    });
    const [editingPermissionId, setEditingPermissionId] = useState(null);


    const fetchData = async () => {
        try {
            const [permsRes, typesRes] = await Promise.all([
                axios.get(`${API_BASE}/api/permissions/GetPermissions`),
                axios.get(`${API_BASE}/api/permissions/GetPermissionTypes`)

            ]);

            setPermissions(permsRes.data);
            setPermissionTypes(typesRes.data);
        } catch (err) {
            console.error('Error al cargar datos:', err);
        }

    };


    const getTypeDescription = (id) => {
        const type = permissionTypes.find(t => t.id === id);
        return type ? type.description : 'Desconocido';
    };

    useEffect(() => {
        fetchData();
    }, []);


    const getTypeDescriptionAux = (id) => {
        const type = permissionTypes.find(t => t.id === id);
        return type ? type.description : 'Desconocido';
    };

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (editingPermissionId) {
                await axios.put(`${API_BASE}/api/permissions/modify/${editingPermissionId}`, form);
                alert("Permiso modificado correctamente");
            } else {
                await axios.post(`${API_BASE}/api/permissions/request`, form);
                alert("Permiso solicitado correctamente");
            }

            fetchData(); // Refresca tabla
           // resetForm();
        } catch (error) {
            console.error("Error al enviar el permiso:", error);
            alert("Error al procesar el permiso.");
        }
    };

    return (

        <div>
            <Box component="form" onSubmit={handleSubmit} sx={{ maxWidth: 500, mx: "auto", p: 2 }}>
                <Typography variant="h6" gutterBottom>
                    {editingPermissionId ? "Modificar Permiso" : "Solicitar Permiso"}
                </Typography>
                <TextField
                    name="employeeFirstName"
                    label="Nombre"
                    value={form.employeeFirstName}
                    onChange={handleChange}
                    fullWidth
                    margin="normal"
                    required
                />
                <TextField
                    name="employeeLastName"
                    label="Apellido"
                    value={form.employeeLastName}
                    onChange={handleChange}
                    fullWidth
                    margin="normal"
                    required
                />
                <TextField
                    name="permissionTypeId"
                    label="Tipo de Permiso"
                    select
                    value={form.permissionTypeId}
                    onChange={handleChange}
                    fullWidth
                    margin="normal"
                    required
                >
                    {permissionTypes.map((type) => (
                        <MenuItem key={type.id} value={type.id}>
                            {type.description}
                        </MenuItem>
                    ))}
                </TextField>
                <TextField
                    name="permissionDate"
                    label="Fecha de Permiso"
                    type="date"
                    value={form.permissionDate}
                    onChange={handleChange}
                    fullWidth
                    margin="normal"
                    InputLabelProps={{ shrink: true }}
                    required
                />
                <Button variant="contained" color="primary" type="submit" fullWidth sx={{ mt: 2 }}>
                    {editingPermissionId ? "Modificar" : "Solicitar"}
                </Button>
                {/*{editingPermissionId && (*/}
                {/*    <Button variant="outlined" color="secondary" onClick={resetForm} fullWidth sx={{ mt: 1 }}>*/}
                {/*        Cancelar Edición*/}
                {/*    </Button>*/}
                {/*)}*/}
            </Box>

            <h1>Permisos Grid Test</h1>

            <Box sx={{ mt: 4, mx: 'auto', maxWidth: '90%' }}>
                <Typography variant="h6" gutterBottom>
                    Permisos
                </Typography>

                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Nombre</TableCell>
                                <TableCell>Apellido</TableCell>
                                <TableCell>Tipo</TableCell>
                                <TableCell>Fecha</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {permissions.length === 0 ? (
                                <TableRow>
                                    <TableCell colSpan={4}>No hay datos disponibles.</TableCell>
                                </TableRow>
                            ) : (
                                permissions.map((p) => (
                                    <TableRow key={p.id}>
                                        <TableCell>{p.employeeFirstName}</TableCell>
                                        <TableCell>{p.employeeLastName}</TableCell>
                                        <TableCell>{getTypeDescription(p.permissionTypeId)}</TableCell>
                                        <TableCell>{new Date(p.permissionDate).toLocaleDateString()}</TableCell>
                                    </TableRow>
                                ))
                            )}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Box>

            <Box sx={{ mt: 4, mx: 'auto', maxWidth: '50%' }}>
                <Typography variant="h6" gutterBottom>
                    Tipos de Permisos
                </Typography>

                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Id</TableCell>
                                <TableCell>Descripcion</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {permissionTypes.length === 0 ? (
                                <TableRow>
                                    <TableCell colSpan={4}>No hay datos disponibles.</TableCell>
                                </TableRow>
                            ) : (
                                permissionTypes.map((p) => (
                                    <TableRow key={p.id}>
                                        <TableCell>{p.id}</TableCell>
                                        <TableCell>{p.description}</TableCell>
                                    </TableRow>
                                ))
                            )}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Box>
        </div>
    );
};

export default Home;