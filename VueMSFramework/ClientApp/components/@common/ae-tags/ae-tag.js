export default {
    props: [
        'page',
        'section',
        'field',
        'viewModel',
        'state',
    ],
    data: function () {
        return {};
    },
    /** *****************************************************************
    *
    * computed
    *
    * _auth
    * _applying
    *
    ******************************************************************* */
    computed: {
        _id: function () {
            return this.section + "-" + this.field.name;
        },
    },
    methods: {
        /* ******************************************************************
         *
         * listener
         *
         * param  string           section
         * param  {}               field{action}
         * param  {}               viewModel{..}
         * $emit  teller
         *
         ******************************************************************* */
        listener: function (section, field, param) {
            this.$emit('teller', section, field, param);
        },
        /* ******************************************************************
        *
        * setBreadcrumb
        * Description
        *
        * param     [{text,to}]  param
        * return    void
        *
        ******************************************************************* */
        setBreadcrumb: function (param) {
            this.$emit('setBreadcrumb', param);
        },
        /* ******************************************************************
        *
        * onFileChange
        *
        * param  {}
        * return void
        *
        ******************************************************************* */
        onFileChange: function (e) {
            console.log("onFileChange");
            var files = e.target.files || e.dataTransfer.files;
            this.createImage(e.target.name, files[0]);
        },
        /* ******************************************************************
        *
        * createImage
        *
        * param  string           name
        * param  {}               file
        * return void
        *
        ******************************************************************* */
        createImage: function (name, file) {
            var _this = this;
            this.viewModel[name] = new Object();
            this.viewModel[name].fileName = "";
            this.viewModel[name].fileType = "";
            this.viewModel[name].base64StringContents = "";
            var reader = new FileReader();
            reader.onload = function (e) {
                _this.viewModel[name].fileName = file.name;
                _this.viewModel[name].fileType = file.type;
                _this.viewModel[name].base64StringContents = e.target.result;
                if (_this.viewModel[name].base64StringContents.length > 15000000) {
                    alert('10Mbyteを超えるファイルは取り込めません。');
                    _this.viewModel[name].fileName = "";
                    _this.viewModel[name].fileType = "";
                    _this.viewModel[name].base64StringContents = "";
                    _this.loading = false;
                    return;
                }
                _this.$http.post('/api/' + _this.page + '/upload', _this.viewModel[name])
                    .then(function (response) {
                    _this.$set(_this.viewModel, name, response.data);
                    //親へイベントをかえす
                    _this.listener(_this.section, { action: "uploaded" }, _this.viewModel);
                })
                    .catch(function (error) {
                    alert(error);
                })
                    .finally(function () {
                    _this.loading = false;
                    _this.show = false;
                    _this.$nextTick(function () { return _this.show = true; });
                });
            };
            reader.readAsDataURL(file);
        },
        /** *****************************************************************
         *
          * fontColor
          *
          * param  string            code
          * return string            code
         *
         ******************************************************************* */
        fontColor: function (code) {
            if (code) {
                var red = parseInt(code.substring(1, 3), 16);
                var green = parseInt(code.substring(3, 5), 16);
                var blue = parseInt(code.substring(5, 7), 16);
                var meido = (red * 299 + green * 587 + blue * 114) / 1000;
                if (meido < 125) {
                    return "white";
                }
                else {
                    return "black";
                }
            }
            else {
                return "white";
            }
        },
        /** *****************************************************************
        *
         * onContextMenu
         *
         * param  string           action
         * param  string               id
         * return void
        *
        ******************************************************************* */
        onContextMenu: function (action, id) {
            if (action === "edit") {
                var currentPage = this.$route.query;
                var nextPage = Object.create(currentPage);
                //var nextPage = { query: Object.assign({ group: id }, currentPage) };
                //this.$router.push(nextPage)
                nextPage["group"] = id;
                this.listener("Group", "activate", id, { query: nextPage });
            }
        },
    }
};
//# sourceMappingURL=ae-tag.js.map