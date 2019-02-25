<template>
    <div class="d-inline">
        <template v-if="field.type === 'text'">
            <label :class="field.name" :for="_id">
                <span v-html="field.caption" />
            </label>
            <b-form-input :id="_id"
                          :name="field.name"
                          :ref="field.name"
                          :type="field.type"
                          :class="field.name"
                          size="lg"
                          v-model="viewModel[field.name]"
                          @input="validator(field)"
                          @change="listener(section,field,viewModel)"
                          :inputMode="field.inputMode"
                          :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                          :minlength="field.minLength"
                          :maxlength="field.maxLength"
                          :required="field.required"
                          :state="state[field.name]"
                          :pattern="field.pattern"
                          :placeholder="field.placeholder"
                          :autofocus="field.autofocus"
                          :readonly="field.readonly"
                          :tabindex="field.readonly?-1:0">

            </b-form-input>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>
        </template>
        <template v-else-if="field.type === 'textarea'">
            <label :class="field.name" :for="_id">
                <span v-html="field.caption" />
            </label>
            <b-form-textarea :id="_id"
                             :class="field.name"
                             :name="field.name"
                             :ref="field.name"
                             v-model="viewModel[field.name]"
                             @input="validator(field)"
                             @change="listener(section,field,viewModel)"
                             :inputMode="field.inputMode"
                             :placeholder="field.placeholder"
                             size="lg"
                             rows="3"
                             max-rows="10"
                             :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                             :minlength="field.minLength"
                             :maxlength="field.maxLength"
                             :required="field.required"
                             :state="state[field.name]"
                             :pattern="field.pattern"
                             :autofocus="field.autofocus"
                             :readonly="field.readonly"
                             :tabindex="field.readonly?-1:0">
            </b-form-textarea>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>
        </template>
        <template v-else-if="field.type === 'password'">
            <label :class="field.name" :for="_id"><span v-html="field.caption" /></label>
            <b-form-input :id="_id"
                          :class="field.name"
                          :name="field.name"
                          :ref="field.name"
                          :type="field.type"
                          size="lg"
                          v-model="viewModel[field.name]"
                          @input="validator(field)"
                          @change="listener(section,field,viewModel)"
                          :inputMode="field.inputMode"
                          :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                          :minlength="field.minLength"
                          :maxlength="field.maxLength"
                          :required="field.required"
                          :state="state[field.name]"
                          :pattern="field.pattern"
                          :placeholder="field.placeholder"
                          :autofocus="field.autofocus"
                          :readonly="field.readonly"
                          :tabindex="field.readonly?-1:0">
            </b-form-input>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>
        </template>
        <template v-else-if="field.type === 'email'">
            <label :class="field.name" :for="_id"><span v-html="field.caption" /></label>
            <b-form-input :id="_id"
                          :name="field.name"
                          :ref="field.name"
                          :type="field.type"
                          size="lg"
                          v-model="viewModel[field.name]"
                          @input="validator(field)"
                          @change="listener(section,field,viewModel)"
                          :inputMode="field.inputMode"
                          :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                          :minlength="field.minLength"
                          :maxlength="field.maxLength"
                          :required="field.required"
                          :state="state[field.name]"
                          :pattern="field.pattern"
                          :placeholder="field.placeholder"
                          :autofocus="field.autofocus"
                          :readonly="field.readonly"
                          :class="field.name"
                          :multiple="field.multiple">
            </b-form-input>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>

        </template>
        <template v-else-if="field.type === 'tel'">
            <label :class="field.name" :for="_id"><span v-html="field.caption" /></label>
            <b-form-input :id="_id"
                          :name="field.name"
                          :ref="field.name"
                          :type="field.type"
                          size="lg"
                          v-model="viewModel[field.name]"
                          @input="validator(field)"
                          @change="listener(section,field,viewModel)"
                          :inputMode="field.inputMode"
                          :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                          :minlength="field.minLength"
                          :maxlength="field.maxLength"
                          :required="field.required"
                          :state="state[field.name]"
                          :pattern="field.pattern"
                          :placeholder="field.placeholder"
                          :autofocus="field.autofocus"
                          :readonly="field.readonly"
                          :class="field.name">
            </b-form-input>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>

        </template>
        <template v-else-if="field.type === 'number'">
            <label :class="field.name" :for="_id"><span v-html="field.caption" /></label>
            <b-form-input :id="_id"
                          :step="field.decimalPointStep"
                          :name="field.name"
                          :ref="field.name"
                          :type="field.type"
                          size="lg"
                          v-model="viewModel[field.name]"
                          @input="validator(field)"
                          @change="listener(section,field,viewModel)"
                          :inputMode="field.inputMode"
                          :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                          :min="field.minRange"
                          :max="field.maxRange"
                          :required="field.required"
                          :state="state[field.name]"
                          :pattern="field.pattern"
                          :placeholder="field.placeholder"
                          :autofocus="field.autofocus"
                          :readonly="field.readonly"
                          :class="field.name">
            </b-form-input>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>

        </template>
        <template v-else-if="field.type === 'date'">
            <label :class="field.name" :for="_id"><span v-html="field.caption" /></label>
            <b-form-input :id="_id"
                          :name="field.name"
                          :ref="field.name"
                          :type="field.type"
                          size="lg"
                          v-model="viewModel[field.name]"
                          @input="validator(field)"
                          @change="listener(section,field,viewModel)"
                          :inputMode="field.inputMode"
                          :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                          :required="field.required"
                          :state="state[field.name]"
                          :placeholder="field.placeholder"
                          :autofocus="field.autofocus"
                          :readonly="field.readonly"
                          :class="field.name">
            </b-form-input>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>

        </template>
        <template v-else-if="field.type === 'datetime'">
            <label :class="field.name" :for="_id">
                <span v-html="field.caption" />
            </label>
            <b-form-input :id="_id"
                          :name="field.name"
                          :ref="field.name"
                          type="datetime-local"
                          size="lg"
                          v-model="viewModel[field.name]"
                          @input="validator(field)"
                          @change="listener(section,field,viewModel)"
                          :inputMode="field.inputMode"
                          :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                          :required="field.required"
                          :state="state[field.name]"
                          :placeholder="field.placeholder"
                          :autofocus="field.autofocus"
                          :readonly="field.readonly"
                          :class="field.name">
            </b-form-input>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>

        </template>
        <template v-else-if="field.type === 'color'">
            <label :class="field.name" :for="_id"><span v-html="field.caption" /></label>
            <b-form-input :id="_id"
                          :name="field.name"
                          :ref="field.name"
                          :type="field.type"
                          size="lg"
                          v-model="viewModel[field.name]"
                          @input="validator(field)"
                          @change="listener(section,field,viewModel)"
                          :inputMode="field.inputMode"
                          :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                          :minlength="field.minLength"
                          :maxlength="field.maxLength"
                          :required="field.required"
                          :state="state[field.name]"
                          :pattern="field.pattern"
                          :placeholder="field.placeholder"
                          :autofocus="field.autofocus"
                          :readonly="field.readonly"
                          :class="field.name">
            </b-form-input>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>

        </template>
        <template v-else-if="field.type === 'file'">
            <label :class="field.name" :for="_id">
                <span v-html="field.caption" />
            </label>
            <b-form-file v-if="!field.readonly"
                         :id="_id"
                         :name="field.name"
                         :ref="field.name"
                         :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                         :placeholder="field.placeholder"
                         :state="state[field.name]"
                         @change="onFileChange"
                         :class="field.name"
                         :multiple="field.multiple">
            </b-form-file>
            <b-form-text v-if="field.help" :id="_id + '-help'">
                <span v-html="field.help" />
            </b-form-text>
            <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                <span v-html="field.invalid" />
            </b-form-invalid-feedback>

            <div v-if="viewModel[field.name]">
                <div class="mt-3">
                    <p>file: <a :href="viewModel[field.name].url" target="_blank">{{viewModel[field.name].fileName}}</a> / {{viewModel[field.name].fileSizeMBorKB}}</p>
                </div>
                <!-- 読み込み直後の場合　-->
                <div v-if="viewModel[field.name].fileType.match('.*image.*')!=null">
                    <div style="max-height:160px;overflow:auto">
                        <img v-if="viewModel[field.name].fileName"
                             :src="viewModel[field.name].url" style="max-width:100%" />
                    </div>
                </div>
                <div v-else-if="viewModel[field.name].fileType.match('.*data:application/pdf.*')!=null">
                    <div style="max-height:160px;overflow:auto">
                        <object style="background-color:silver;margin:0;padding:0;max-height:160px;max-width:300px;"
                                :data="viewModel[field.name].url"
                                :type="viewModel[field.name].fileType"></object>
                    </div>
                </div>
                <div v-else-if="viewModel[field.name].fileType.match('.*data:text/plain.*')!=null">
                    <div style="max-height:160px;overflow:auto">
                        <object style="background-color:silver;margin:0;padding:0;max-height:160px;max-width:300px;"
                                :data="viewModel[field.name].url"
                                :type="viewModel[field.name].fileType"></object>
                    </div>
                </div>
                <div v-else>
                    <!--<p style="color:grey;font-size:0.8em">この形式のファイルはプレビュー表示できません。</p>-->
                </div>
                <br />
            </div>

        </template>
        <template v-else-if="field.type === 'checkbox'">
            <label :class="field.name" :for="_id">
                <span v-html="field.caption" />{{viewModel[field.name]}}
            </label>
            <b-form-group>
                <b-button v-if="field.readonly"
                          :id="_id"
                          :name="field.name"
                          :ref="field.name"
                          :pressed="viewModel[field.name]"
                          variant="outline-success"
                          :readonly="field.readonly"
                          @click="return false;"
                          key="outline-success">
                    <span v-html="field.caption" />
                </b-button>
                <b-button v-else
                          :id="_id"
                          :name="field.name"
                          :ref="field.name"
                          :pressed.sync="viewModel[field.name]"
                          variant="outline-success"
                          :readonly="field.readonly"
                          key="outline-success">
                    <span v-html="field.caption" />
                </b-button>
                <b-form-text v-if="field.help" :id="_id + '-help'">
                    <span v-html="field.help" />
                </b-form-text>
                <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                    <span v-html="field.invalid" />
                </b-form-invalid-feedback>

            </b-form-group>
        </template>
        <template v-else-if="field.type === 'checkboxes'">
            <template v-if="viewModel.options[field.name]">

                <div @contextmenu.prevent="$refs.contextMenu.open(arguments[0],$event)" style="display:inline-block">
                    <b-form-group :label="field.caption">
                        <b-form-checkbox-group :name="field.name"
                                               size="lg"
                                               :required="field.required"
                                               :readonly="field.readonly"
                                               @input="listener(section,field,viewModel)"
                                               v-model="viewModel[field.name]"
                                               :options="viewModel.options[field.name]">
                        </b-form-checkbox-group>
                    </b-form-group>
                </div>
            </template>
            <template v-else-if="field.options">
                <div @contextmenu.prevent="$refs.contextMenu.open(arguments[0],$event)" style="display:inline-block">
                    <b-form-group :label="field.caption">
                        <b-form-checkbox-group :name="field.name"
                                               size="lg"
                                               :required="field.required"
                                               :readonly="field.readonly"
                                               @input="listener(section,field,viewModel)"
                                               v-model="viewModel[field.name]"
                                               :options="field.options">
                        </b-form-checkbox-group>
                    </b-form-group>
                </div>
            </template>
        </template>
        <template v-else-if="field.type === 'radio'">
            <template v-if="viewModel.options[field.name]">

                <label :class="field.name" :for="_id"><span v-html="field.caption" /></label>
                <b-form-group>
                    <b-form-radio-group :id="_id"
                                        :name="field.name"
                                        size="lg"
                                        :required="field.required"
                                        :readonly="field.readonly"
                                        v-model="viewModel[field.name]"
                                        :options="viewModel.options[field.name]" />
                </b-form-group>
                <b-form-text v-if="field.help" :id="_id + '-help'">
                    <span v-html="field.help" />
                </b-form-text>
                <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                    <span v-html="field.invalid" />
                </b-form-invalid-feedback>


            </template>
            <template v-else-if="field.options">

                <label :class="field.name" :for="_id"><span v-html="field.caption" /></label>
                <b-form-group>
                    <b-form-radio-group :id="_id"
                                        :name="field.name"
                                        size="lg"
                                        :required="field.required"
                                        :readonly="field.readonly"
                                        v-model="viewModel[field.name]"
                                        :options="field.options" />
                </b-form-group>
                <b-form-text v-if="field.help" :id="_id + '-help'">
                    <span v-html="field.help" />
                </b-form-text>
                <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                    <span v-html="field.invalid" />
                </b-form-invalid-feedback>

            </template>
        </template>
        <template v-else-if="field.type === 'radios'">
            <template v-if="viewModel.options[field.name]">
                <label :class="field.name" :for="_id">
                    <span v-html="field.caption" />
                </label>
                <b-form-group>
                    <b-form-radio-group :id="_id"
                                        size="lg"
                                        v-model="viewModel[field.name]"
                                        :readonly="field.readonly"
                                        :options="viewModel.options[field.name]"
                                        :name="field.name" />
                </b-form-group>
                <b-form-text v-if="field.help" :id="_id + '-help'">
                    <span v-html="field.help" />
                </b-form-text>
                <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                    <span v-html="field.invalid" />
                </b-form-invalid-feedback>

            </template>
            <template v-else-if="field.options">
                <label :class="field.name" :for="_id">
                    <span v-html="field.caption" />
                </label>
                <b-form-group>
                    <b-form-radio-group :id="_id"
                                        size="lg"
                                        v-model="viewModel[field.name]"
                                        :readonly="field.readonly"
                                        :options="field.options"
                                        :name="field.name" />
                </b-form-group>
                <b-form-text v-if="field.help" :id="_id + '-help'">
                    <span v-html="field.help" />
                </b-form-text>
                <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                    <span v-html="field.invalid" />
                </b-form-invalid-feedback>

            </template>
        </template>
        <template v-else-if="field.type === 'toggle'">
            <template v-if="viewModel.options[field.name]">
                <label :class="field.name" :for="_id">
                    <span v-html="field.caption" />
                </label>
                <b-form-group>
                    <b-form-radio-group :id="_id"
                                        size="lg"
                                        buttons
                                        button-variant="outline-success"
                                        v-model="viewModel[field.name]"
                                        :readonly="field.readonly"
                                        :options="viewModel.options[field.name]"
                                        :name="field.name" />
                </b-form-group>
                <b-form-text v-if="field.help" :id="_id + '-help'">
                    <span v-html="field.help" />
                </b-form-text>
                <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                    <span v-html="field.invalid" />
                </b-form-invalid-feedback>
            </template>
            <template v-else-if="field.options">
                <label :class="field.name" :for="_id">
                    <span v-html="field.caption" />
                </label>
                <b-form-group>
                    <b-form-radio-group :id="_id"
                                        size="lg"
                                        buttons
                                        button-variant="outline-success"
                                        :readonly="field.readonly"
                                        v-model="viewModel[field.name]"
                                        :options="field.options"
                                        :name="field.name" />
                </b-form-group>
                <b-form-text v-if="field.help" :id="_id + '-help'">
                    <span v-html="field.help" />
                </b-form-text>
                <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                    <span v-html="field.invalid" />
                </b-form-invalid-feedback>

            </template>
        </template>
        <template v-else-if="field.type === 'select'">
            <template v-if="field.options">
                <label :class="field.name" :for="_id"><span v-html="field.caption" /></label>
                <b-form-select v-model="viewModel[field.name]"
                               :ref="field.name"
                               :options="field.options"
                               size="lg"
                               :id="_id"
                               :aria-describedby="_id + '-help  ' + _id + '-feedback'"
                               :required="field.required"
                               :autofocus="field.autofocus"
                               :readonly="field.readonly"
                               :class="field.name">
                </b-form-select>
                <b-form-text v-if="field.help" :id="_id + '-help'">
                    <span v-html="field.help" />
                </b-form-text>
                <b-form-invalid-feedback v-if="field.invalid" :id="_id + '-feedback'">
                    <span v-html="field.invalid" />
                </b-form-invalid-feedback>


            </template>
        </template>


    </div>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {


            /* ******************************************************************
            *
            * validator
            * Description   Validity check of input items
            *
            * param     {}  field
            * return    void
            *
            ******************************************************************* */
            validator: function (field) {

                if (!field) return
                if (!field.name) return

                //const $el = document[this.section][field.name];
                const $el = document.getElementById(this._id);

                if ($el.checkValidity()) {
                    this.$set(this.state, field.name, true);
                } else {
                    this.$set(this.state, field.name, false);
                }


            },
            /* ******************************************************************
            *
            * listener
            * Description
            *
            * param     {}  field
            * return    void
            *
            ******************************************************************* */
            listener: function (section,field,param) {

                if (!field) return
                if (!field.name) return
                if (!field.event || !field.event.action) return

                if (!this.state[field.name]) return;

                const $el = document[this.section];
                if (!$el.checkValidity()) {
                    return;
                }

                //this.listener(this.section, field, this.viewModel)
                this.$emit('teller', section, field, param)

                return;
            },


        },

    }

</script>
<style scoped>
</style>
