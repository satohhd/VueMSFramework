const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const bundleOutputDir = './wwwroot/dist';
process.traceDeprecation = true;
module.exports = (env) => {
    const isDevBuild = !(process.env && process.env.NODE_ENV === 'production');
    //const isDevBuild = false;
    console.log("isDevBuild=" + isDevBuild)
    return [{
        stats: { modules: false },
        entry: { 'main': './ClientApp/boot-app.js' },
        resolve: {
            extensions: ['.js', '.vue'],
            alias: {
                'vue$': 'vue/dist/vue',
                'components': path.resolve(__dirname, './ClientApp/components'),
                'views': path.resolve(__dirname, './ClientApp/views'),
                'utils': path.resolve(__dirname, './ClientApp/utils'),
                'api': path.resolve(__dirname, './ClientApp/store/api')
            }
        },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: '/dist/'
        },
        module: {
            rules: [
                {
                    test: /\.vue$/,
                    loader: 'vue-loader',
                    options: {
                        loaders: {
                            scss: 'vue-style-loader!css-loader!sass-loader', // <style lang="scss">
                            sass: 'vue-style-loader!css-loader!sass-loader?indentedSyntax', // <style lang="sass">
                        }
                        // other vue-loader options go here
                    }
                },
                {
                    test: /\.less$/,
                    use: [
                        'vue-style-loader',
                        'css-loader',
                        'less-loader'
                    ]
                },
                { test: /\.js$/, include: /ClientApp/, use: 'babel-loader' },
                { test: /\.scss?$/, loaders: ['style-loader', 'css-loader', 'sass-loader'] },
                { test: /\.css$/, use: isDevBuild ? ['style-loader', 'css-loader'] : ExtractTextPlugin.extract({ use: 'css-loader' }) },
//                { test: /\.css$/, loaders: ['style-loader', 'css-loader'] },
                { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' },
//                { test: /\.(woff|woff2|eot|ttf|svg)$/,loader: 'file-loader?name=../font/[name].[ext]'}
                { test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, loader: 'url-loader?mimetype=image/svg+xml' },
                { test: /\.woff(\d+)?(\?v=\d+\.\d+\.\d+)?$/, loader: 'url-loader?mimetype=application/font-woff' },
                { test: /\.eot(\?v=\d+\.\d+\.\d+)?$/, loader: 'url-loader?mimetype=application/font-woff' },
                { test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/, loader: 'url-loader?mimetype=application/font-woff' }

            ]
        },
        plugins: [
            new webpack.DefinePlugin({
                //'process.env.NODE_ENV': isDevBuild ? JSON.stringify('development') : JSON.stringify('production')
                'process.env.NODE_ENV': JSON.stringify('production')
            }),
            new UglifyJsPlugin(),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            })
        ].concat(isDevBuild ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
            })
        ] : [
                // Plugins that apply in production builds only
                new webpack.optimize.UglifyJsPlugin(),
                new ExtractTextPlugin('site.css')
            ])
    }];
};
