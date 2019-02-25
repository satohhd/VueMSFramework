<template>
    <b-form @submit.stop.prevent ="checkForm" :ref="section" :name="section" v-if="show">
        <b-container fluid>
            <b-row>
                <template v-for="field in fields">
                    <b-col v-if="field.tag" :sm="field.gridCol" :offset-sm="field.gridOffset">
                        <component :page="page"
                                   :section="section"
                                   :field="field"
                                   :viewModel="viewModel"
                                   :state="state" :is="uiTagPrefix + field.tag"
                                   @setBreadcrumb="setBreadcrumb"
                                   @teller="listener"></component>
                    </b-col>
                </template>
            </b-row>
        </b-container>
    </b-form>
</template>

<script>
    /* ******************************************************************
    *
    * ae-dynamic-section-form
    *
    * in  string    page
    * in  string    section
    * in  {}        fields
    * in  {}        viewModelClassName
    *
    ******************************************************************* */
    import aeTransition from "./ae-transition-utils.js"
    export default {
        mixins: [aeTransition],
        props: [
            'page',
            'section',
            'fields',
            'viewModel',
        ],
        data() {
            return {
                state: {},
                show: true,
                focusField: null,
            }
        },
        watch: {
            /* ******************************************************************
             *
             * watch focusField
             * Description
             *
             ******************************************************************* */
            "focusField": function (e, old) {
                if (e) {
                    if (e == undefined || e == null) return;
                    if (e == old) return;
                    if (document.getElementById(e)) {
                        setTimeout(function (id) {
                            document.getElementById(e).focus();
                        }, 200, e);
                    }
                }
            },
            /* ******************************************************************
            *
            * watch viewModel
            * Description
            *
            ******************************************************************* */
            "viewModel": function (to, from) {
                if (to == from) return;
                this.state = {}
                this.show = false
                this.$nextTick(() => this.show = true)
            }
        },
        methods: {
           /* ******************************************************************
            *
            * checkForm
            * Description
            *
            * param     {}  event
            * return    bool true/false
            *
            ******************************************************************* */
            checkForm: function (evt) {

                if (evt) {
                    evt.preventDefault();
                }
                const form = this.$refs[this.section];
                if (!form.checkValidity()) {
                    var id = null;
                    for (var f in form.elements) {
                        this.state[form.elements[f].name] = true;
                        if (form.elements[f].checkValidity) {
                            if (!form.elements[f].checkValidity()) {
                                this.state[form.elements[f].name] = false;
                                if (!id) {
                                    name = form.elements[f].id;
                                }
                            }
                        }
                    }
                    if (name) {
                        this.focusField = id;
                    }
                    this.show = false
                    this.$nextTick(() => {
                        this.show = true
                    })
                    return false;
                }
                return true;
            },

            /* ******************************************************************
             *
             * listener (overraide)
             *
             * param  string           section
             * param  {}               field{action}
             * param  {}               viewModel{..}
             * return void
             *
             ******************************************************************* */
            listener: function (section, field, param) {
                console.log({ section, field, param })
                if (!field.event) return;
                if (this.section === section) {

                    //入力チェック
                    if (field.isVerify) {

                        //単項目チェック
                        var ret = this.checkForm();
                        if (!ret) {
                            console.log("invlid")
                            return;
                        }

                    }
                    //確認ダイアログ
                    if (field.isConfirm) {
                        if (!confirm(field.confirmMessage)) {
                           console.log("cancel")
                           return;
                        }
                    }

                }
                if (param) {
                    if (0 === Object.keys(param).length) {
                        this.$emit('teller', section, field, this.viewModel)
                    } else {
                      this.$emit('teller', section, field, param)
                    }
                } else {
                    //var vm = this.refiner(this.fields,this,this.viewModel)
                  this.$emit('teller', section, field, this.viewModel)
                }

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
                this.$emit('setBreadcrumb', param)
            },


 
        },
        created: function () {
        },
        mounted: function () {
        },
    }

</script>
<style scoped>
</style>
