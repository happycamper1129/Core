var path = require('path');
var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var extractCSS = new ExtractTextPlugin('vendor.css');
var isDevelopment = process.env.ASPNETCORE_ENVIRONMENT === 'Development';

module.exports = {
    resolve: {
        extensions: ['', '.js']
    },
    module: {
        loaders: [
            { test: /\.(png|woff|woff2|eot|ttf|svg)$/, loader: 'url-loader?limit=100000' },
            { test: /\.css/, loader: extractCSS.extract(['css']) },
            { test: /\.json$/, loader: 'json-loader' }
        ]
    },
    entry: {
        vendor: [
            'font-awesome/css/font-awesome.css',
            'bootstrap/dist/css/bootstrap.css',
            'style-loader',
            '@angular/common',
            '@angular/compiler',
            '@angular/core',
            '@angular/http',
            '@angular/forms',
            '@angular/platform-browser',
            '@angular/platform-browser-dynamic',
            '@angular/router'
        ]
    },
    output: {
        path: path.join(__dirname, '../wwwroot', 'dist'),
        filename: '[name].js',
        library: '[name]_[hash]',
    },
    plugins: [
        extractCSS,
        new webpack.optimize.OccurrenceOrderPlugin(),
        new webpack.DllPlugin({
            path: path.join(__dirname, '../wwwroot', 'dist', '[name]-manifest.json'),
            name: '[name]_[hash]'
        })
    ].concat(isDevelopment ? [] : [
        new webpack.optimize.UglifyJsPlugin({
            compress: { warnings: false },
            minimize: true,
            mangle: false // Due to https://github.com/angular/angular/issues/6678
        })
    ])
};
