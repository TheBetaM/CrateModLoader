using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class AnimationEditor : Form
    {
        private SectionController controller;
        private Animation animation;
        private Animation.BoneSettings BoneSettings;
        private Animation.BoneSettings BoneSettings2;
        private Animation.Timeline Timeline;
        private Animation.Timeline Timeline2;
        private Animation.Transformation Transformation;
        private Animation.Transformation Transformation2;

        public AnimationEditor(SectionController c)
        {
            controller = c;
            InitializeComponent();
            Text = $"Animation editor";
            tbTimeline.TickStyle = TickStyle.Both;
            tbTimeline2.TickStyle = TickStyle.Both;
            tbTimeline.Enabled = false;
            tbTimeline2.Enabled = false;
            PopulateList();
        }

        private void PopulateList()
        {
            lbAnimations.SelectedIndex = -1;
            lbAnimations.Items.Clear();
            foreach (Animation anim in controller.Data.Records)
            {
                lbAnimations.Items.Add($"Animation ID {anim.ID}");
            }
        }

        private void PopulateWithAnimData(IList list, IList data, Action<IList, string[], int> adder, params string[] namePattern)
        {
            list.Clear();
            var index = 1;
            foreach (var d in data)
            {
                adder.Invoke(list, namePattern, index++);
            }
        }

        private void lbAnimations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1) return;
            BoneSettings = null;
            BoneSettings2 = null;
            Timeline = null;
            Timeline2 = null;
            Transformation = null;
            Transformation2 = null;
            tbTimeline.Enabled = false;
            tbTimeline2.Enabled = false;
            tbDisB1.Text = "";
            tbDisB2.Text = "";
            tbDisB3.Text = "";
            tbDisB4.Text = "";
            tbDisB5.Text = "";
            tbDisB6.Text = "";
            tbDisB7.Text = "";
            tbDisB8.Text = "";
            tbDis2B1.Text = "";
            tbDis2B2.Text = "";
            tbDis2B3.Text = "";
            tbDis2B4.Text = "";
            tbDis2B5.Text = "";
            tbDis2B6.Text = "";
            tbDis2B7.Text = "";
            tbDis2B8.Text = "";
            tbTransformation.Text = "";
            tbTransformation2.Text = "";
            animation = (Animation)controller.Data.Records[lbAnimations.SelectedIndex];
            UpdateLists();
        }

        private void UpdateLists()
        {
            Action<IList, string[], int> listAdder = (list, name, index) =>
            {
                list.Add($"{name[0]} {index}");
            };
            Action<IList, string[], int> transformationAdder = (list, namePattern, index) =>
            {
                var ind = index - 1;
                var nameFormat = namePattern[ind % namePattern.Length];
                var param0 = (ind / 8) >= 1 ? "bone" : "model";
                var param1 = (ind / 8) == 0 ? "" : (ind / 8).ToString();

                list.Add(string.Format(nameFormat, param0, param1));
            };
            string[] tranformationPattern =
            {
                "Translate {0} {1} X",
                "Translate {0} {1} Z",
                "Translate {0} {1} Y",
                "Rotate {0} {1} X",
                "Rotate {0} {1} Z",
                "Rotate {0} {1} Y",
                "Scale {0} {1} X",
                "Scale {0} {1} Y"
            };
            PopulateWithAnimData(lbBoneSettings.Items, animation.BonesSettings, listAdder, "Bone setting");
            PopulateWithAnimData(lbTransformations.Items, animation.Transformations, transformationAdder, tranformationPattern);
            PopulateWithAnimData(lbTimelines.Items, animation.Timelines, listAdder, "Timeline");
            PopulateWithAnimData(lbBoneSettings2.Items, animation.BonesSettings2, listAdder, "Bone setting");
            PopulateWithAnimData(lbTransformations2.Items, animation.Transformations2, transformationAdder, tranformationPattern);
            PopulateWithAnimData(lbTimelines2.Items, animation.Timelines2, listAdder, "Timeline");
        }

        private void lbDisplacements_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListBox)sender;
            if (list.SelectedIndex == -1) return;

            Animation.BoneSettings displacement = animation.BonesSettings[list.SelectedIndex];
            BoneSettings = displacement;
            tbDisB1.Text = displacement.Unknown[0].ToString();
            tbDisB2.Text = displacement.Unknown[1].ToString();
            tbDisB3.Text = displacement.Unknown[2].ToString();
            tbDisB4.Text = displacement.Unknown[3].ToString();
            tbDisB5.Text = displacement.Unknown[4].ToString();
            tbDisB6.Text = displacement.Unknown[5].ToString();
            tbDisB7.Text = displacement.Unknown[6].ToString();
            tbDisB8.Text = displacement.Unknown[7].ToString();
        }

        private void lbScales_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListBox)sender;
            if (list.SelectedIndex == -1) return;

            Animation.Transformation scale = animation.Transformations[list.SelectedIndex];
            Transformation = scale;
            tbTransformation.Text = scale.Value.ToString();
        }

        private void lbRotations_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListBox)sender;
            if (list.SelectedIndex == -1) return;

            Animation.Timeline timeline = animation.Timelines[list.SelectedIndex];
            Timeline = timeline;
            tbTimeline.Enabled = true;
            tbTimeline.Value = 0;
            tbTimeline.Minimum = 0;
            tbTimeline.Maximum = timeline.UnknownInt16s.Count - 1;
            tbTimeline_Scroll(sender, e);
        }

        private void lbDisplacements2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListBox)sender;
            if (list.SelectedIndex == -1) return;

            Animation.BoneSettings displacement = animation.BonesSettings2[list.SelectedIndex];
            BoneSettings2 = displacement;
            tbDis2B1.Text = displacement.Unknown[0].ToString();
            tbDis2B2.Text = displacement.Unknown[1].ToString();
            tbDis2B3.Text = displacement.Unknown[2].ToString();
            tbDis2B4.Text = displacement.Unknown[3].ToString();
            tbDis2B5.Text = displacement.Unknown[4].ToString();
            tbDis2B6.Text = displacement.Unknown[5].ToString();
            tbDis2B7.Text = displacement.Unknown[6].ToString();
            tbDis2B8.Text = displacement.Unknown[7].ToString();
        }

        private void lbScales2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListBox)sender;
            if (list.SelectedIndex == -1) return;

            Animation.Transformation scale = animation.Transformations2[list.SelectedIndex];
            Transformation2 = scale;
            tbTransformation2.Text = scale.Value.ToString();
        }

        private void lbRotations2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListBox)sender;
            if (list.SelectedIndex == -1) return;

            Animation.Timeline timeline = animation.Timelines2[list.SelectedIndex];
            Timeline2 = timeline;
            tbTimeline2.Enabled = true;
            tbTimeline2.Value = 0;
            tbTimeline2.Minimum = 0;
            tbTimeline2.Maximum = timeline.UnknownInt16s.Count - 1;
            tbTimeline2_Scroll(sender, e);
        }

        private void tbDisB1_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings == null) return;
            BoneSettings.Unknown[0] = result;
        }

        private void tbDisB2_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings == null) return;
            BoneSettings.Unknown[1] = result;
        }

        private void tbDisB3_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings == null) return;
            BoneSettings.Unknown[2] = result;
        }

        private void tbDisB4_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings == null) return;
            BoneSettings.Unknown[3] = result;
        }

        private void tbDisB5_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings == null) return;
            BoneSettings.Unknown[4] = result;
        }

        private void tbDisB6_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings == null) return;
            BoneSettings.Unknown[5] = result;
        }

        private void tbDisB7_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings == null) return;
            BoneSettings.Unknown[6] = result;
        }

        private void tbDisB8_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings == null) return;
            BoneSettings.Unknown[7] = result;
        }

        private void tbTransformation_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Single.TryParse(tb.Text, out Single result) || Transformation == null) return;
            Transformation.Value = result;
        }

        private void tbDis2B1_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings2 == null) return;
            BoneSettings2.Unknown[0] = result;
        }

        private void tbDis2B2_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings2 == null) return;
            BoneSettings2.Unknown[1] = result;
        }

        private void tbDis2B3_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings2 == null) return;
            BoneSettings2.Unknown[2] = result;
        }

        private void tbDis2B4_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings2 == null) return;
            BoneSettings2.Unknown[3] = result;
        }

        private void tbDis2B5_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings2 == null) return;
            BoneSettings2.Unknown[4] = result;
        }

        private void tbDis2B6_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings2 == null) return;
            BoneSettings2.Unknown[5] = result;
        }

        private void tbDis2B7_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings2 == null) return;
            BoneSettings2.Unknown[6] = result;
        }

        private void tbDis2B8_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Byte.TryParse(tb.Text, out Byte result) || BoneSettings2 == null) return;
            BoneSettings2.Unknown[7] = result;
        }

        private void tbTransformation2_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Single.TryParse(tb.Text, out Single result) || Transformation2 == null) return;
            Transformation2.Value = result;
        }

        private void tbTimeline_Scroll(object sender, EventArgs e)
        {
            lblFrame.Text = $"Frame: {tbTimeline.Value}";
            tbTransformOffset.Text = Timeline.GetOffset(tbTimeline.Value).ToString();
        }

        private void tbTransformOffset_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Int16.TryParse(tb.Text, out Int16 result) || Timeline == null) return;
            Timeline.SetOffset(tbTimeline.Value, result);
        }

        private void tbTimeline2_Scroll(object sender, EventArgs e)
        {
            lblFrame2.Text = $"Frame: {tbTimeline2.Value}";
            tbTransformOffset2.Text = Timeline2.GetOffset(tbTimeline2.Value).ToString();
        }

        private void tbTransformOffset2_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (!Int16.TryParse(tb.Text, out Int16 result) || Timeline2 == null) return;
            Timeline2.SetOffset(tbTimeline2.Value, result);
        }

        private void btnAddTransformation_Click(object sender, EventArgs e)
        {
            if (animation == null) return;
            animation.Transformations.Add(new Animation.Transformation());
            UpdateLists();
        }

        private void btnDeleteTransformation_Click(object sender, EventArgs e)
        {
            if (animation == null) return;
            animation.Transformations.RemoveAt(animation.Transformations.Count - 1);
            UpdateLists();
        }

        private void btnAddTimeline_Click(object sender, EventArgs e)
        {
            if (animation == null) return;
            animation.Timelines.Add(new Animation.Timeline(animation.TimelineLength1));
            for (var i = 0; i < animation.Timelines[animation.Timelines.Count - 1].UnknownInt16s.Capacity; ++i)
            {
                animation.Timelines[animation.Timelines.Count - 1].UnknownInt16s.Add(0);
            }
            UpdateLists();
        }

        private void btnDeleteTimeline_Click(object sender, EventArgs e)
        {
            if (animation == null) return;
            animation.Timelines.RemoveAt(animation.Timelines.Count - 1);
            UpdateLists();
        }

        private void btnAddTransformation2_Click(object sender, EventArgs e)
        {
            if (animation == null) return;
            animation.Transformations2.Add(new Animation.Transformation());
            UpdateLists();
        }

        private void btnDeleteTransformation2_Click(object sender, EventArgs e)
        {
            if (animation == null) return;
            animation.Transformations2.RemoveAt(animation.Transformations2.Count - 1);
            UpdateLists();
        }

        private void btnAddTimeline2_Click(object sender, EventArgs e)
        {
            if (animation == null) return;
            if (animation.TimelineLength2 == 0)
            {
                animation.TimelineLength2 = 1;
            }
            animation.Timelines2.Add(new Animation.Timeline(animation.TimelineLength2));
            for (var i = 0; i < animation.Timelines2[animation.Timelines2.Count - 1].UnknownInt16s.Capacity; ++i)
            {
                animation.Timelines2[animation.Timelines2.Count - 1].UnknownInt16s.Add(0);
            }
            UpdateLists();
        }

        private void btnDeleteTimeline2_Click(object sender, EventArgs e)
        {
            if (animation == null) return;
            animation.Timelines2.RemoveAt(animation.Timelines2.Count - 1);
            UpdateLists();
        }
    }
}
